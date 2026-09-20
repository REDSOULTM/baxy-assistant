param([int]$Limit = 40)
$ErrorActionPreference='Stop'
Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes
Add-Type @'
using System;
using System.Runtime.InteropServices;
public static class BaxyVisibleListNative {
  private delegate bool EnumProc(IntPtr hwnd, IntPtr lParam);
  [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
  [DllImport("user32.dll")] private static extern bool EnumWindows(EnumProc callback, IntPtr value);
  [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr hwnd);
  [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint procId);
  [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hwnd, out RECT rect);
  [StructLayout(LayoutKind.Sequential)] public struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
  // Declaradas antes de la llamada: el compilador que trae Add-Type en Windows
  // PowerShell 5.1 no admite «out uint x» en linea.
  public static IntPtr LargestVisible(IntPtr hwnd) {
    // H0050 y las demas de video: esta regla cambiaba la ventana de delante
    // por la mas grande del mismo proceso. Con un navegador de varias
    // ventanas eso lee la equivocada —medido: con Netflix delante devolvia
    // la de YouTube, por ser mayor—. La ventana de delante es la que la
    // persona esta mirando; la regla del area sigue para cuando esa ventana
    // no sirve como raiz: un marco degenerado o sin superficie.
    RECT self;
    if (GetWindowRect(hwnd, out self)) {
      long own = (long)Math.Max(0, self.Right - self.Left) * Math.Max(0, self.Bottom - self.Top);
      if (own >= 200 * 200) return hwnd;
    }
    uint procId;
    GetWindowThreadProcessId(hwnd, out procId);
    IntPtr best=hwnd; long bestArea=0;
    EnumWindows((top,unused)=>{
      if(!IsWindowVisible(top)) return true;
      uint owner;
      GetWindowThreadProcessId(top, out owner);
      if(owner!=procId) return true;
      RECT r; if(!GetWindowRect(top,out r)) return true;
      long area=(long)Math.Max(0,r.Right-r.Left)*Math.Max(0,r.Bottom-r.Top);
      if(area>bestArea){bestArea=area;best=top;}
      return true;
    },IntPtr.Zero);
    return best;
  }
}
'@
function Emit([bool]$ok,[string]$error,[string]$window,$controls,[int]$total,[string]$process){
  [pscustomobject]@{
    version=1
    ok=$ok
    error=$error
    window=$window
    process=$process
    controlCount=$total
    controls=@($controls)
    authority='windows_uia_snapshot'
  }|ConvertTo-Json -Compress -Depth 4
}
# CU1959: el motor general necesita saber en que estado esta cada control
# (marcado, seleccionado, desplegado, con foco, con valor) y en que zona de la
# ventana cae, para elegir un paso sin ver la pantalla.
function Get-State($el){
  $parts=@()
  try {
    $pattern=$null
    if($el.TryGetCurrentPattern([System.Windows.Automation.TogglePattern]::Pattern,[ref]$pattern)){
      $state=([System.Windows.Automation.TogglePattern]$pattern).Current.ToggleState
      if($state -eq [System.Windows.Automation.ToggleState]::On){$parts+='on'} elseif($state -eq [System.Windows.Automation.ToggleState]::Off){$parts+='off'}
    }
    if($el.TryGetCurrentPattern([System.Windows.Automation.SelectionItemPattern]::Pattern,[ref]$pattern)){
      if(([System.Windows.Automation.SelectionItemPattern]$pattern).Current.IsSelected){$parts+='selected'}
    }
    if($el.TryGetCurrentPattern([System.Windows.Automation.ExpandCollapsePattern]::Pattern,[ref]$pattern)){
      $state=([System.Windows.Automation.ExpandCollapsePattern]$pattern).Current.ExpandCollapseState
      if($state -eq [System.Windows.Automation.ExpandCollapseState]::Expanded){$parts+='expanded'} elseif($state -eq [System.Windows.Automation.ExpandCollapseState]::Collapsed){$parts+='collapsed'}
    }
    if($el.TryGetCurrentPattern([System.Windows.Automation.ValuePattern]::Pattern,[ref]$pattern)){
      $value=[string]([System.Windows.Automation.ValuePattern]$pattern).Current.Value
      if(-not [string]::IsNullOrWhiteSpace($value)){ $value=($value -replace '\s+',' ').Trim(); if($value.Length -gt 40){$value=$value.Substring(0,40)}; $parts+=('value='+$value) }
    }
    if($el.Current.HasKeyboardFocus){$parts+='focused'}
  } catch [System.Windows.Automation.ElementNotAvailableException] {}
  return ($parts -join ',')
}
function Get-Zone($el,$bounds){
  try {
    $r=$el.Current.BoundingRectangle
    if($r.IsEmpty -or $bounds.Width -le 0 -or $bounds.Height -le 0){ return '' }
    $cx=($r.X+$r.Width/2-$bounds.X)/$bounds.Width
    $cy=($r.Y+$r.Height/2-$bounds.Y)/$bounds.Height
    $row=if($cy -lt 0.33){'top'}elseif($cy -lt 0.66){'middle'}else{'bottom'}
    $col=if($cx -lt 0.33){'left'}elseif($cx -lt 0.66){'center'}else{'right'}
    return ($row+'-'+$col)
  } catch { return '' }
}
try {
  if($Limit -lt 1){$Limit=1}
  if($Limit -gt 60){$Limit=60}
  $hwnd=[BaxyVisibleListNative]::GetForegroundWindow()
  if($hwnd -eq [IntPtr]::Zero){ Emit $false 'active_window_not_found' '' @() 0 ''; exit 2 }
  $hwnd=[BaxyVisibleListNative]::LargestVisible($hwnd)
  $root=[System.Windows.Automation.AutomationElement]::FromHandle($hwnd)
  if($null -eq $root){ Emit $false 'visible_controls_root_unavailable' '' @() 0 ''; exit 3 }
  $windowName=$root.Current.Name
  $processName=''
  try { [uint32]$procId=0; [void][BaxyVisibleListNative]::GetWindowThreadProcessId($hwnd,[ref]$procId); $processName=(Get-Process -Id $procId -ErrorAction Stop).ProcessName } catch {}
  $bounds=$root.Current.BoundingRectangle
  $enabled=New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::IsEnabledProperty,$true)
  $all=$root.FindAll([System.Windows.Automation.TreeScope]::Descendants,$enabled)
  # Lo que una persona pulsa o escribe va primero. Los contenedores —paneles,
  # grupos, imagenes, texto suelto— son la mayoria de lo que devuelve UIA y no se
  # pueden accionar: con un modelo de 4096 de contexto, gastan el sitio de lo que
  # si sirve. Se conservan detras, por si la ventana no tiene otra cosa.
  $actionable=@('Button','MenuItem','ListItem','TabItem','Hyperlink','CheckBox',
                'RadioButton','Edit','ComboBox','TreeItem','SplitButton','Slider')
  $seen=New-Object 'System.Collections.Generic.HashSet[string]'
  $primary=@()
  $secondary=@()
  $total=0
  foreach($item in $all){
    try {
      if($item.Current.IsOffscreen){ continue }
      $name=$item.Current.Name
      if([string]::IsNullOrWhiteSpace($name)){ continue }
      $name=($name -replace '\s+',' ').Trim()
      if($name.Length -gt 80){ $name=$name.Substring(0,80) }
      # Un mismo nombre repetido no informa de nada nuevo y gasta el contexto del
      # modelo: se nombra una vez. La ambiguedad la sigue detectando el clic.
      $zone=Get-Zone $item $bounds
      # El mismo nombre en otra zona es otro control (dos «Biblioteca», el del
      # menu y el titulo); en la misma zona es un duplicado.
      $key=$item.Current.ControlType.ProgrammaticName+'|'+$name+'|'+$zone
      if(-not $seen.Add($key)){ continue }
      $total++
      $kind=$item.Current.ControlType.ProgrammaticName -replace '^ControlType\.',''
      $state=Get-State $item
      $controlId=''
      try { $controlId=($item.GetRuntimeId() -join '.') } catch {}
      $entry=[pscustomobject]@{name=$name;kind=$kind;state=$state;zone=$zone;controlId=$controlId}
      if($actionable -contains $kind){ $primary+=@($entry) } else { $secondary+=@($entry) }
    } catch [System.Windows.Automation.ElementNotAvailableException] {}
  }
  $controls=@($primary)
  if($controls.Count -lt $Limit){
    $controls+=@($secondary | Select-Object -First ($Limit-$controls.Count))
  }
  $controls=@($controls | Select-Object -First $Limit)
  $indexed=@()
  $position=0
  foreach($entry in $controls){ $position++; $entry | Add-Member -NotePropertyName index -NotePropertyValue $position; $indexed+=@($entry) }
  Emit $true '' $windowName $indexed $total $processName
} catch { Emit $false 'visible_controls_uia_failed' '' @() 0 ''; exit 7 }
