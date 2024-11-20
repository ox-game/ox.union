@Echo Off
echo Only supports x64 windows
echo  Create desktop shortcut
mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(a.SpecialFolders(""Desktop"") & ""\OX.Union.lnk""):b.TargetPath=""%~dp0OX.Union.exe"":b.IconLocation =""%~dp0favicon.ico"":b.WorkingDirectory=""%~dp0"":b.Save:close")
mshta VBScript:Execute("Set a=CreateObject(""WScript.Shell""):Set b=a.CreateShortcut(a.SpecialFolders(""Desktop"") & ""\Union.Helper.lnk""):b.TargetPath=""%~dp0Union.Helper.exe"":b.IconLocation =""%~dp0favicon2.ico"":b.WorkingDirectory=""%~dp0"":b.Save:close")
echo install windowsdesktop-runtime-7.0.20-win-x64
windowsdesktop-runtime-7.0.20-win-x64
echo install aspnetcore-runtime-7.0.20-win-x64
aspnetcore-runtime-7.0.20-win-x64