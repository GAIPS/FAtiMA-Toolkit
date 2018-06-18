@echo off

SET origin=bin\Debug
SET include=*.exe *.dll config6
SET exclude=*vshost.exe *vshost.dll

ROBOCOPY "%origin%" "%~f1" %include% /XF %exclude% /S /NP /NFL /NDL /NJH /NJS