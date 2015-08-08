@echo off
cd /D %~dp0
powershell "Set-ExecutionPolicy Unrestricted -Force"
xcopy /E /Y /I PSWindowsUpdate %WINDIR%\System32\WindowsPowerShell\v1.0\Modules\PSWindowsUpdate
powershell .\config.ps1
pause