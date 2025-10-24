<#
.SYNOPSIS
  Toggle or set Windows 11 dark/light mode and notify running apps.

.DESCRIPTION
  Updates the registry keys that control Windows and app theme:
    HKCU\...\Personalize\SystemUsesLightTheme
    HKCU\...\Personalize\AppsUseLightTheme
  Then broadcasts WM_SETTINGCHANGE with "ImmersiveColorSet" so apps refresh.

.PARAMETER Mode
  Dark   -> sets dark mode
  Light  -> sets light mode
  Toggle -> flips current mode

.PARAMETER Verbose
  Use -Verbose to see what the script is doing.

.NOTES
  Teams must be set to Appearance = "Default" to follow the system theme.
#>

[CmdletBinding()]
param(
  [Parameter(Mandatory=$true)]
  [ValidateSet('Dark','Light','Toggle')]
  [string]$Mode
)

# --- Constants / paths ---
$PersonalizePath = 'HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize'
$AppsUseLightName = 'AppsUseLightTheme'
$SystemUseLightName = 'SystemUsesLightTheme'

function Get-CurrentMode {
  try {
    $appsLight = (Get-ItemProperty -Path $PersonalizePath -Name $AppsUseLightName -ErrorAction Stop).$AppsUseLightName
  } catch {
    # Default to Light (1) if key doesn't exist yet
    $appsLight = 1
  }
  if ($appsLight -eq 0) { 'Dark' } else { 'Light' }
}

function Set-ThemeValues([string]$targetMode) {
  $value = if ($targetMode -eq 'Dark') { 0 } else { 1 }
  Write-Verbose "Setting $AppsUseLightName = $value"
  New-Item -Path $PersonalizePath -Force | Out-Null
  Set-ItemProperty -Path $PersonalizePath -Name $AppsUseLightName -Value $value -Type DWord -Force
  Write-Verbose "Setting $SystemUseLightName = $value"
  Set-ItemProperty -Path $PersonalizePath -Name $SystemUseLightName -Value $value -Type DWord -Force
}

function Send-ThemeBroadcastFast {
  $code = @"
using System;
using System.Runtime.InteropServices;

public static class NativeFast {
  [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
  public static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

  [DllImport("user32.dll", CharSet=CharSet.Auto)]
  public static extern IntPtr SendMessageTimeout(
      IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam,
      uint fuFlags, uint uTimeout, out UIntPtr lpdwResult);
}
"@
  if (-not ([System.Management.Automation.PSTypeName]'NativeFast').Type) {
    Add-Type -TypeDefinition $code
  }

  $HWND_BROADCAST = [IntPtr]0xffff
  $WM_SETTINGCHANGE = 0x1A

  # Fire-and-forget (returns immediately)
  [NativeFast]::SendNotifyMessage($HWND_BROADCAST, $WM_SETTINGCHANGE, [UIntPtr]::Zero, [IntPtr]::Zero) | Out-Null

  # Also send the two informative lParams, but use quick timeouts & "abort if hung"
  $SMTO_ABORTIFHUNG = 0x0002
  $SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
  $flags = $SMTO_ABORTIFHUNG -bor $SMTO_NOTIMEOUTIFNOTHUNG
  $result = [UIntPtr]::Zero

  [NativeFast]::SendMessageTimeout($HWND_BROADCAST, $WM_SETTINGCHANGE, [UIntPtr]::Zero, 'ImmersiveColorSet', $flags, 100, [ref]$result) | Out-Null
  [NativeFast]::SendMessageTimeout($HWND_BROADCAST, $WM_SETTINGCHANGE, [UIntPtr]::Zero, 'Software\Microsoft\Windows\CurrentVersion\Themes\Personalize', $flags, 100, [ref]$result) | Out-Null
}


# --- Apply & broadcast ---
Set-ThemeValues -targetMode $Mode
Send-ThemeBroadcastFast

Write-Host "Theme set to $Mode."
