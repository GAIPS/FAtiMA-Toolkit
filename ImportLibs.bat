::first parameter states if requires debug or release version of libs
::second parameter is the target directory to copy the libs
::(current content of the folder is deleted before copying)

@echo off
SET LibFolderCache=ExportDlls

IF NOT "%~1" == "debug" (
	IF NOT "%~1" == "release" (
		echo First Parameter must contain "debug" or "release" string
		exit /B
	)
)

SET TargetDir=%~f2

pushd %~p0

CALL "ExportDlls.bat" %1 "%LibFolderCache%"

IF "%~2"=="" exit /b

IF EXIST "%TargetDir%" (
	RD "%TargetDir%" /S /Q
)
MD "%TargetDir%"

ROBOCOPY "%LibFolderCache%" "%TargetDir%" /S /NP /NFL /NDL /NJH /NJS

popd