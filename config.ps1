##########################################
# Windows 10 Configuration               #
# (c) 2015 by                            #
# virtualmarc.info                       #
# Licensed under MIT License             #
##########################################

# Windows Update Module
Import-Module PSWindowsUpdate

# REG_BINARY
$REG_BINARY = [String]"Binary"
# REG_DWORD
$REG_DWORD = [String]"DWord"
# REG_EXPAND_SZ
$REG_EXPAND_SZ = [String]"ExpandString"
# REG_MULTI_SZ
$REG_MULTI_SZ = [String]"MultiString"
# REG_QWORD
$REG_QWORD = [String]"QWord"
# REG_STRING
$REG_SZ = [String]"String"

# ENV Vars
$SYSTEM_ROOT = (get-item env:SystemRoot).Value
$USER_PROFILE = (get-item env:UserProfile).Value
$LOCAL_APPDATA = (get-item env:LocalAppData).Value
$PROGRAM_DATA = (get-item env:ProgramData).Value

# Function to create a Registry Key, if not exist
Function W10RKCheck($key) {
    If (-not (Test-Path "Registry::$key")) {
        New-Item -Path "Registry::$key" -ItemType RegistryKey -Force
    }
}

# Function to write a Registry Property
Function W10RPWrite($key,$name,$value,$type) {
    New-ItemProperty -Path "Registry::$key" -Name $name -Value $value -PropertyType $type -Force
}

# Function to delete a Registry Property
Function W10RPDelete($key,$name) {
    Remove-ItemProperty -Path "Registry::$key" -Name $name -Force | Out-Null
}

# Function to delete a Registry Key
Function W10RKDelete($key) {
    Remove-Item -Path "Registry::$key" -Force -Recurse | Out-Null
}

# Function to check if option was selected
Function W10Active($prop,$default) {
    If ($prop -eq "") {
        $prop = $default
    }
    return ($prop -eq "y") -or ($prop -eq "Y") -or ($prop -eq "j") -or ($prop -eq "J")
}

# Header
Write-Output "Starting Windows 10 Configuration"
Write-Output "(c) 2015 by virtualmarc.info"
Write-Output "Run at your own risk"
Write-Output "I assume no liability for damages or loss of data"
Write-Output "Licensed under MIT License"
Write-Output "This Script was tested with Windows 10 Pro, not Home or Enterprise"
$continue = Read-Host "Continue? (y/N)"
If (-not (W10Active $continue "n")) {
    Write-Error "Cancelled by user!"
    exit 1
}

# PC Owner
$i_owner = Read-Host "PC Owner Name"

# OneDrive
$i_onedrive = Read-Host "Deactivate OneDrive? (Y/n)"

# NVIDIA Drivers from Windows Update
$i_nvidia = Read-Host "Deactivate NVIDIA Drivers from Windows Update? (Y/n)"

# Synaptics & Elantech Drivers
$i_touchpad = Read-Host "Deactivate Touchpad Drivers (Synaptics & Elantech) from Windows Update? (y/N)"

# OneNote
$i_onenote = Read-Host "Uninstall OneNote App? (Y/n)"

# XBOX
$i_xbox = Read-Host "Uninstall XBOX App? (Y/n)"

# Contacts
$i_people = Read-Host "Uninstall Contacts App? (Y/n)"

# GetSkype
$i_skype = Read-Host "Uninstall GetSkype App? (Y/n)"

# Microsoft Solitaire Collection
$i_solitaire = Read-Host "Uninstall Microsoft Solitaire Collection App? (Y/n)"

# Get Office
$i_office = Read-Host "Uninstall GetOffice App? (Y/n)"

# GetStarted
$i_started = Read-Host "Uninstall GetStarted App? (Y/n)"

# Bing Sports
$i_sports = Read-Host "Uninstall Bing Sports App? (Y/n)"

# Bing News
$i_news = Read-Host "Uninstall Bing News App? (Y/n)"

# Bing Finance
$i_finance = Read-Host "Uninstall Bing Finance App? (Y/n)"

# Bing Weather
$i_wetter = Read-Host "Uninstall Bing Weather App? (Y/n)"

# 3D Builder
$i_3dbuilder = Read-Host "Uninstall 3D Builder App? (Y/n)"

# Mail
$i_mail = Read-Host "Uninstall Mail App? (Y/n)"

# Windows Mobile
$i_mobile = Read-Host "Uninstall Windows Mobile App? (Y/n)"

# Camera
$i_kamera = Read-Host "Uninstall Camera App? (y/N)"

# Maps
$i_maps = Read-Host "Uninstall Maps App? (y/N)"

# Windows Feedback
$i_feedback = Read-Host "Uninstall Windows Feedback App? (y/N)"

# Film & TV
$i_video = Read-Host "Uninstall Film & TV App? (y/N)"

# Photos
$i_fotos = Read-Host "Uninstall Photos App? (y/N)"

# Music
$i_musik = Read-Host "Uninstall Music App? (y/N)"

# Lock Screen
$i_lockscreen = Read-Host "Deactivate Lock Screen? (y/N)"

# Cortana
$i_cortana = Read-Host "Deactivate Cortana? (Y/n)"

# Websearch
$i_websearch = Read-Host "Deactivate Websearch? (Y/n)"

# Bandwidthlimit
$i_bandwidth = Read-Host "Disable Bandwidth limit? (Y/n)"

# Ask run for .exe and msi
$i_askrun = Read-Host "Deactivate Prompt to run .exe and .msi Files? (Y/n)"

# Programcompatibility Service
$i_compatibility = Read-Host "Deactivate Prompt if setup was successful? (Y/n)"

# Websearch for unknown files
$i_unknowntype = Read-Host "Deactivate Websearch for unknown files? (Y/n)"

# Icon margin
$i_iconmargin = Read-Host "Change Icon Margin on Desktop? (Y/n)"
$iconmargin_h = [String]""
$iconmargin_v = [String]""
If (W10Active $i_iconmargin "y") {
    $iconmargin_h = Read-Host "Horizontal Margin [Win10: -1710 / Win7: -1710] (-1710)"
    If ($iconmargin_h -eq "") {
        $iconmargin_h = "-1710"
    }
    $iconmargin_v = Read-Host "Vertical Margin [Win10: -1710 / Win7: -1125] (-1125)"
    If ($iconmargin_v -eq "") {
        $iconmargin_v = "-1125"
    }
}

# Privacy
$i_privacy = Read-Host "Change Privacy Settings? (Y/n)"

# Startmenu
$i_startmenu = Read-Host "Disable last used Apps in Startmenu? (Y/n)"

# Tipps for Windows
$i_tipps = Read-Host "Disable Tipps for Windows? (Y/n)"

# Windows Update
$i_wu = Read-Host "Configure Windows Updates? (Y/n)"

# OneDrive
If (W10Active $i_onedrive "y") {
    Write-Output "Deactivating and removing OneDrive"
    $OneDriveRegKey = [String]"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Group Policy Objects\{983DA565-B39A-46B5-97EB-7BE5D54E9D19}Machine\SOFTWARE\Policies\Microsoft\Windows\OneDrive"
    $OneDriveRegProperty = [String]"DisableFileSyncNGSC"
    W10RKCheck $OneDriveRegKey
    W10RPWrite $OneDriveRegKey $OneDriveRegProperty 0x1 $REG_DWORD
    taskkill /f /im OneDrive.exe
    Start-Process "$SYSTEM_ROOT\SysWOW64\OneDriveSetup.exe" -ArgumentList "/uninstall" -wait -NoNewWindow -PassThru
    Remove-Item "$USER_PROFILE\OneDrive" -Force -Recurse | Out-Null
    Remove-Item "$LOCAL_APPDATA\Microsoft\OneDrive" -Force -Recurse | Out-Null
    Remove-Item "$PROGRAM_DATA\Microsoft OneDrive" -Force -Recurse | Out-Null
    Remove-Item c:\OneDriveTemp -Force -Recurse | Out-Null
    $OneDriveExplorerRegKey = [String]"HKCR\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}"
    $OneDriveExplorer64RegKey = [String]"HKCR\Wow6432Node\CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}"
    $OneDriveExplorerRegProp = [String]"IsPinnedToNameSpaceTree"
    $OneDriveExplorerRegPropSys = [String]"System.IsPinnedToNameSpaceTree"
    W10RKDelete $OneDriveExplorerRegKey
    W10RKDelete $OneDriveExplorer64RegKey
    W10RKCheck $OneDriveExplorerRegKey
    W10RKCheck $OneDriveExplorer64RegKey
    W10RPWrite $OneDriveExplorerRegKey $OneDriveExplorerRegProp 0x0 $REG_DWORD
    W10RPWrite $OneDriveExplorerRegKey $OneDriveExplorerRegPropSys 0x0 $REG_DWORD
    W10RPWrite $OneDriveExplorer64RegKey $OneDriveExplorerRegProp 0x0 $REG_DWORD
    W10RPWrite $OneDriveExplorer64RegKey $OneDriveExplorerRegPropSys 0x0 $REG_DWORD
}

# Default Apps
Write-Output "Uninstalling Default Apps:"
If (W10Active $i_onenote "y") {
    Write-Output "OneNote App"
    Get-AppxPackage -AllUsers *microsoft.office.onenote* | Remove-AppxPackage
    Get-AppxPackage *microsoft.office.onenote* | Remove-AppxPackage
}
If (W10Active $i_xbox "y") {
    Write-Output "XBOX App"
    Get-AppxPackage -AllUsers *microsoft.xboxapp* | Remove-AppxPackage
    Get-AppxPackage *microsoft.xboxapp* | Remove-AppxPackage
}
If (W10Active $i_people "y") {
    Write-Output "Contacts App"
    Get-AppxPackage -AllUsers *microsoft.people* | Remove-AppxPackage
    Get-AppxPackage *microsoft.people* | Remove-AppxPackage
}
If (W10Active $i_skype "y") {
    Write-Output "GetSkype App"
    Get-AppxPackage -AllUsers *microsoft.skypeapp* | Remove-AppxPackage
    Get-AppxPackage *microsoft.skypeapp* | Remove-AppxPackage
}
If (W10Active $i_solitaire "y") {
    Write-Output "Microsoft Solitaire Collection App"
    Get-AppxPackage -AllUsers *microsoft.microsoftsolitairecollection* | Remove-AppxPackage
    Get-AppxPackage *microsoft.microsoftsolitairecollection* | Remove-AppxPackage
}
If (W10Active $i_office "y") {
    Write-Output "GetOffice App"
    Get-AppxPackage -AllUsers *microsoft.microsoftofficehub* | Remove-AppxPackage
    Get-AppxPackage *microsoft.microsoftofficehub* | Remove-AppxPackage
}
If (W10Active $i_started "y") {
    Write-Output "GetStarted App"
    Get-AppxPackage -AllUsers *microsoft.getstarted* | Remove-AppxPackage
    Get-AppxPackage *microsoft.getstarted* | Remove-AppxPackage
}
If (W10Active $i_sports "y") {
    Write-Output "Bing Sports App"
    Get-AppxPackage -AllUsers *microsoft.bingsports* | Remove-AppxPackage
    Get-AppxPackage *microsoft.bingsports* | Remove-AppxPackage
}
If (W10Active $i_news "y") {
    Write-Output "Bing News App"
    Get-AppxPackage -AllUsers *microsoft.bingnews* | Remove-AppxPackage
    Get-AppxPackage *microsoft.bingnews* | Remove-AppxPackage
}
If (W10Active $i_finance "y") {
    Write-Output "Bing Finance App"
    Get-AppxPackage -AllUsers *microsoft.bingfinance* | Remove-AppxPackage
    Get-AppxPackage *microsoft.bingfinance* | Remove-AppxPackage
}
If (W10Active $i_wetter "y") {
    Write-Output "Bing Weather App"
    Get-AppxPackage -AllUsers *microsoft.bingweather* | Remove-AppxPackage
    Get-AppxPackage *microsoft.bingweather* | Remove-AppxPackage
}
If (W10Active $i_3dbuilder "y") {
    Write-Output "3D Builder App"
    Get-AppxPackage -AllUsers *microsoft.3dbuilder* | Remove-AppxPackage
    Get-AppxPackage *microsoft.3dbuilder* | Remove-AppxPackage
}
If (W10Active $i_mail "y") {
    Write-Output "Mail App"
    Get-AppxPackage -AllUsers *microsoft.windowscommunicationsapps* | Remove-AppxPackage
    Get-AppxPackage *microsoft.windowscommunicationsapps* | Remove-AppxPackage
}
If (W10Active $i_mobile "y") {
    Write-Output "Windows Mobile App"
    Get-AppxPackage -AllUsers *microsoft.windowsmobile* | Remove-AppxPackage
    Get-AppxPackage *microsoft.windowsmobile* | Remove-AppxPackage
}
If (W10Active $i_kamera "n") {
    Write-Output "Camera App"
    Get-AppxPackage -AllUsers *microsoft.windowscamera* | Remove-AppxPackage
    Get-AppxPackage *microsoft.windowscamera* | Remove-AppxPackage
}
If (W10Active $i_maps "n") {
    Write-Output "Maps App"
    Get-AppxPackage -AllUsers *microsoft.windowsmaps* | Remove-AppxPackage
    Get-AppxPackage *microsoft.windowsmaps* | Remove-AppxPackage
}
If (W10Active $i_feedback "n") {
    Write-Output "Windows Feedback App"
    Get-AppxPackage -AllUsers *microsoft.windowsfeedback* | Remove-AppxPackage
    Get-AppxPackage *microsoft.windowsfeedback* | Remove-AppxPackage
}
If (W10Active $i_video "n") {
    Write-Output "Film & TV App"
    Get-AppxPackage -AllUsers *microsoft.zunevideo* | Remove-AppxPackage
    Get-AppxPackage *microsoft.zunevideo* | Remove-AppxPackage
}
If (W10Active $i_fotos "n") {
    Write-Output "Photos App"
    Get-AppxPackage -AllUsers *microsoft.windows.photos* | Remove-AppxPackage
    Get-AppxPackage *microsoft.windows.photos* | Remove-AppxPackage
}
If (W10Active $i_musik "n") {
    Write-Output "Music App"
    Get-AppxPackage -AllUsers *microsoft.zunemusic* | Remove-AppxPackage
    Get-AppxPackage *microsoft.zunemusic* | Remove-AppxPackage
}

# Lock Screen
If (W10Active $i_lockscreen "n") {
    Write-Output "Deactivate Lock Screen"
    $LockScreenRegKey = [String]"HKLM\SOFTWARE\Policies\Microsoft\Windows\Personalization"
    $LockScreenRegProp = [String]"NoLockScreen"
    W10RKCheck $LockScreenRegKey
    W10RPWrite $LockScreenRegKey $LockScreenRegProp 0x1 $REG_DWORD
}

$WinSearchRegKey = [String]"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Group Policy Objects\{983DA565-B39A-46B5-97EB-7BE5D54E9D19}Machine\SOFTWARE\Policies\Microsoft\Windows\Windows Search"

# Cortana
If (W10Active $i_cortana "y") {
    Write-Output "Deactivate Cortana"
    $CortanaRegProp = [String]"AllowCortana"
    W10RKCheck $WinSearchRegKey
    W10RPWrite $WinSearchRegKey $CortanaRegProp 0x0 $REG_DWORD
}

# Websearch
If (W10Active $i_websearch "y") {
    Write-Output "Deactivate Websearch"
    W10RKCheck $WinSearchRegKey
    W10RPWrite $WinSearchRegKey "ConnectedSearchUseWeb" 0x0 $REG_DWORD
    W10RPWrite $WinSearchRegKey "ConnectedSearchUseWebOverMeteredConnections" 0x0 $REG_DWORD
    W10RPWrite $WinSearchRegKey "DisableWebSearch" 0x1 $REG_DWORD
}

# Banwidth limit
If (W10Active $i_bandwidth "y") {
    Write-Output "Deactivate Bandwidth Limit"
    $BandwidthRegKey = [String]"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Group Policy Objects\{983DA565-B39A-46B5-97EB-7BE5D54E9D19}Machine\SOFTWARE\Policies\Microsoft\Windows\Psched"
    $BandwidthRegProp = [String]"NonBestEffortLimit"
    W10RKCheck $BandwidthRegKey
    W10RPWrite $BandwidthRegKey $BandwidthRegProp 0x0 $REG_DWORD
}

# Ask Run for .exe and .msi
If (W10Active $i_askrun "y") {
    Write-Output "Deactivate Prompt to run .exe and .msi Files"
    $AskRunRegKey = [String]"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Group Policy Objects\{983DA565-B39A-46B5-97EB-7BE5D54E9D19}User\Software\Microsoft\Windows\CurrentVersion\Policies\Associations"
    $AskRunRegProp = [String]"LowRiskFileTypes"
    W10RKCheck $AskRunRegKey
    W10RPWrite $AskRunRegKey $AskRunRegProp ".exe;.msi;" $REG_SZ
}

# Programcompatibility Service
If (W10Active $i_compatibility "y") {
    Write-Output "Deactivate Prompt if setup was successful"
    stop-service PcaSvc
    set-service PcaSvc -StartupType Disabled
}

# Websearch for Unknown files
If (W10Active $i_unknowntype "y") {
    Write-Output "Deactivate Websearch for unknown files"
    $UnknownTypeRegKey = [String]"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer"
    $UnknownTypeRegProp = [String]"NoInternetOpenWith"
    W10RKCheck $UnknownTypeRegKey
    W10RPWrite $UnknownTypeRegKey $UnknownTypeRegProp 0x1 $REG_DWORD
}

# Icon margin
If (W10Active $i_iconmargin "y") {
    Write-Output "Setting Icon Margin"
    $IconMarginRegKey = [String]"HKCU\Control Panel\Desktop\WindowMetrics"
    W10RKCheck $IconMarginRegKey
    W10RPWrite $IconMarginRegKey "IconSpacing" "$iconmargin_h" $REG_SZ
    W10RPWrite $IconMarginRegKey "IconVerticalSpacing" "$iconmargin_v" $REG_SZ
}

# Registred Owner
Write-Output "Setting Registred Owner"
$OwnerRegKey = [String]"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion"
W10RKCheck $OwnerRegKey
W10RPWrite $OwnerRegKey "RegisteredOwner" "$i_owner" $REG_SZ
W10RPWrite $OwnerRegKey "RegisteredOrganization" "" $REG_SZ

# Privacy
If (W10Active $i_privacy "y") {
    Write-Output "Setting Privacy Settings"
    # Advertising ID
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo" "Enabled" 0x0 $REG_DWORD
    W10RPDelete "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo" "Id"
    # Smart Screen
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\AppHost" "EnableWebContentEvaluation" 0x0 $REG_DWORD
    # Voice
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Internet Explorer\International"
    W10RPDelete "HKCU\SOFTWARE\Microsoft\Internet Explorer\International" "AcceptLanguage"
    W10RPWrite "HKCU\Control Panel\International\User Profile" "HttpAcceptLanguageOptOut" 0x1 $REG_DWORD
    # Camera
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{E5323777-F976-4f5b-9B55-B94699C46E44}"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{E5323777-F976-4f5b-9B55-B94699C46E44}" "Value" "Deny" $REG_SZ
    # Microphone
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{2EEF81BE-33FA-4800-9670-1CD474972C3F}"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{2EEF81BE-33FA-4800-9670-1CD474972C3F}" "Value" "Deny" $REG_SZ
    # Account
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{C1D23ACC-752B-43E5-8448-8D0E519CD6D6}"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{C1D23ACC-752B-43E5-8448-8D0E519CD6D6}" "Value" "Deny" $REG_SZ
    # Calendar
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{D89823BA-7180-4B81-B50C-7E471E6121A3}"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{D89823BA-7180-4B81-B50C-7E471E6121A3}" "Value" "Deny" $REG_SZ
    # SMS/MMS
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{992AFA70-6F47-4148-B3E9-3003349C1548}"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{992AFA70-6F47-4148-B3E9-3003349C1548}" "Value" "Deny" $REG_SZ
    # Bluetooth
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{A8804298-2D5F-42E3-9531-9C8C39EB29CE}"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\{A8804298-2D5F-42E3-9531-9C8C39EB29CE}" "Value" "Deny" $REG_SZ
    # Sync
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\LooselyCoupled"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeviceAccess\Global\LooselyCoupled" "Value" "Deny" $REG_SZ
    # Feedback
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Siuf\Rules"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Siuf\Rules" "NumberOfSIUFInPeriod" 0x0 $REG_DWORD
    W10RPDelete "HKCU\SOFTWARE\Microsoft\Siuf\Rules" "PeriodInNanoSeconds"
    # Diagnostic and usage data
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap" "AutoDetect" 0x0 $REG_DWORD
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap" "IntranetName" 0x1 $REG_DWORD
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap" "ProxyByPass" 0x1 $REG_DWORD
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap" "UNCAsIntranet" 0x1 $REG_DWORD
}

# Windows Update
If (W10Active $i_wu "y") {
    Write-Output "Configuring Windows Updates"
    W10RKCheck "HKLM\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings"
    # Auto Reboot
    W10RPWrite "HKLM\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings" "UxOption" 0x0 $REG_DWORD
    # Defer Upgrade
    W10RPWrite "HKLM\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings" "DeferUpgrade" 0x0 $REG_DWORD
    # Deactivate Torrent
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization" "SystemSettingsDownloadMode" 0x1 $REG_DWORD
}

# Startmenu
If (W10Active $i_startmenu "y") {
    Write-Output "Deactivate Last opened Apps in Start Menu"
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" "Start_TrackProgs" 0x0 $REG_DWORD
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced" "Start_TrackDocs" 0x0 $REG_DWORD
}

# Tipps for Windows
If (W10Active $i_tipps "y") {
    Write-Output "Deactivating Tipps for Windows"
    W10RKCheck "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\SoftLanding"
    W10RPWrite "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\SoftLanding" "Enabled" 0x0 $REG_DWORD
}

# NVIDIA Drivers from Windows Update
If (W10Active $i_nvidia "y") {
    Write-Output "Deactivating NVIDIA Drivers from Windows Update"
    Hide-WUUpdate -Title "NVIDIA*" -HideStatus:$true -Confirm:$false
}

# Synaptics & Elantech Drivers
If (W10Active $i_touchpad "n") {
    Write-Output "Deactivating Touchpad Drivers"
    Hide-WUUpdate -Title "*Synaptics*" -HideStatus:$true -Confirm:$false
    Hide-WUUpdate -Title "*Elantech*" -HideStatus:$true -Confirm:$false
}

# Wi-Sense
Write-Output "Please deactivate Wi-Send Manually"

# Neustart
Write-Output "You need to restart your system in order to apply the settings!"
$reboot = Read-Host "Restart? (Y/n)"
if (W10Active $reboot "y") {
    shutdown /r /f /t 05
}
