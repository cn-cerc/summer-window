if /i "%PROCESSOR_IDENTIFIER:~0,3%" == "X86" goto 1 
if /i "%PROCESSOR_IDENTIFIER:~0,3%" NEQ "X86" goto 2 
:1
reg add "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION" /v "vine-window-standard.exe" /t REG_DWORD /d "9999" /f

:2  /////64位xp注册install服务
reg.exe add "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION" /v "vine-window-standard.exe" /t REG_DWORD /d "9999"  /f /reg:64 

