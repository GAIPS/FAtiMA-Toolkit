@echo off
set assetRepositoriesFolder=..\AssetDev
set destination=\Authoring Tools
SET mainExportBat=ExportAuthTools.bat

::----------------------------------------------------------

set targetFolder=%cd%%destination%
set repFolder=%cd%\%assetRepositoryFolder%

echo "%targetFolder%"

IF NOT EXIST "%targetFolder%" (
	echo Target folder "%targetFolder%" does not exist
	exit /B
)

del /q /f "%targetFolder%"
FOR /D %%i IN ("%targetFolder%\*") DO (
	RD /S /Q "%%i"
)

pushd %assetRepositoriesFolder%
	echo Collecting all Authoring Tools...
	FOR /D %%i IN (*) DO (
		pushd %%i
			IF EXIST "%mainExportBat%" (
				echo Importing "%%i" Tools...
				set d="%targetFolder%\%%i Tools"
				MKDIR %d%
				CALL %mainExportBat% %d%
				echo "%%i" Tools Importing to "%d%" Completed
			)
		popd
	)
	echo Authoring Tools Collection Completed
popd

