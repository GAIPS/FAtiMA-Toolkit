@echo off
set mode=debug
set assetRepositoriesFolder=..\..\AssetDev
set destination=\Assets\AssetDLLs
SET mainExportBat=ExportDlls.bat

::----------------------------------------------------------

set targetFolder=%cd%%destination%
set repFolder=%cd%\%assetRepositoryFolder%

IF NOT EXIST "%targetFolder%" (
	exit /B Target folder "%targetFolder%" does not exist
)

del /q /f "%targetFolder%"

pushd %assetRepositoriesFolder%
	echo Collecting all Libraries...
	FOR /D %%i IN (*) DO (
		pushd %%i
			IF EXIST "%mainExportBat%" (
				echo Importing "%%i" Asset...
				CALL %mainExportBat% %mode% "%targetFolder%"
				echo Asset "%%i" Importing Completed
			)
		popd
	)
	echo Library Collection Completed
popd
