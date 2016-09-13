@echo off

SET exportBat=export.bat
SET excludeDirectories=\tools\ \Tests\
SET pdo2mdbPath=\tools\pdb2mdb\pdb2mdb.exe

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
	echo Target folder "%targetPath%" does not exist
	exit /B 
)

FOR /F "delims=" %%i IN ('dir /B /S /A:D ^| findstr /v "%excludeDirectories%"') DO (
	pushd "%%~fi"
		IF EXIST %exportBat% (
			echo Exporting %%i
			CALL %exportBat% %~1 "%targetPath%"
		)
	popd
)

IF "%~1" == "debug" (
	echo Converting PDO to MDB...
	pushd %targetPath%
	
	for /r %%i in (*.dll) do (
		IF EXIST %%~ni.pdb (
			echo Converting "%%~i"
			call "%cd%%pdo2mdbPath%" "%%~i"
		)
	)
	
	popd
	echo Convertion Completed!
)

popd