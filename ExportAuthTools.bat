@echo off

SET includeDirectories=GUI
SET exportBat=toolExport.bat

IF "%~1"=="" (
	echo First Parameter does not contain destination path for the export
	exit /B
)

SET targetPath=%~f1

IF NOT EXIST "%targetPath%" (
	echo Target folder "%targetPath%" does not exist
	exit /B 
)

::Set directory to batch file's folder
pushd %~p0

FOR %%i IN (%includeDirectories%) DO (
	IF EXIST %%i (
		pushd %%i
			FOR /F %%j IN ('dir /b /AD') DO (
				pushd "%%~fj"
					IF EXIST %exportBat% (
						echo Exporting %%j
						CALL %exportBat% "%targetPath%"
					)
				popd
			)
		popd
	)
) 

popd