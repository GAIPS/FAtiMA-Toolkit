@echo off

SET exportBat=export.bat

IF NOT "%~1" == "debug" (
	IF NOT "%~1" == "release" (
		echo First Parameter must contain "debug" or "release" string
		exit /B
	)
)

IF "%~2"=="" (
	echo Second Parameter does not contain destination path for the export
	exit /B
)

SET targetPath=%~f2
::Set directory to batch file's folder
pushd %~p0

IF EXIST "%targetPath%" (
	RD "%targetPath%" /S /Q
)
MD "%targetPath%"

FOR /D /r %%i IN (*) DO (
	pushd %%i
	IF EXIST %exportBat% (
		CALL %exportBat% %~1 "%targetPath%"
	)
	popd
)

popd

if "%~1"=="debug" (
	if not "%~3"=="" (
		echo Converting pdb to mdb
		pushd %targetPath%
			FOR %%i IN (*.dll) DO (
				call %3 %%i
			)
		popd
	)
)