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

FOR /D %%i IN (*) DO (
	pushd %%i
	IF EXIST %exportBat% (
		CALL %exportBat% %~1 "%targetPath%"
	)
	popd
)

popd