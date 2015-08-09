@echo off

cd /D %~dp0

xcopy /E /Y /I PSWindowsUpdate %WINDIR%\System32\WindowsPowerShell\v1.0\Modules\PSWindowsUpdate

