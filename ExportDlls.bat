@echo off

SET exportBat=export.bat
SET excludeDirectories=tools Tests

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

IF NOT EXIST "%targetPath%" (
	exit /B Target folder "%targetPath%" does not exist
)

FOR /F %%i IN ('dir /b /AD ^| findstr /v /x "%excludeDirectories%"') DO (
	pushd "%%~fi"
		IF EXIST %exportBat% (
			echo Exporting %%i
			CALL %exportBat% %~1 "%targetPath%"
		)
	popd
)

popd