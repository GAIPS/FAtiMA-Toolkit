@echo off
SET debugPath=bin\Debug
SET debugInclude=*.dll *.pdb *.mdb
SET releasePath=bin\Release
SET releaseInclude=*.dll

IF "%~1"=="debug" (
	SET originPath=%debugPath%
	SET include=%debugInclude%
) ELSE IF "%~1"=="release" (
	SET originPath=%releasePath%
	SET include=%releaseInclude%
) ELSE (
	echo mode flag mismatch
	EXIT /b
)

ROBOCOPY "%originPath%" "%~2" %include% /S /NP /NFL /NDL /NJH /NJS