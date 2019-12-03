

@echo off

IF "%~1"=="" (
	echo First Parameter does not contain destination path for the export
	exit /B
)

SET pdo2mdbPath=\tools\pdb2mdb\pdb2mdb.exe
echo "Exporting Assets..."
for /r "Assets" %%i in (*.dll *.pdb) do xcopy "%%~fi" "%~1" /y >nul
echo "Exporting Components..."
for /r "Components" %%i in (*.dll *.pdb) do xcopy "%%~fi" "%~1" /y >nul
echo "Exporting Utilities..."
for /r "Utilities/SerializationUtilities" %%i in (*.dll *.pdb) do xcopy "%%~fi" "%~1" /y >nul
for /r "Utilities/Utilities" %%i in (*.dll *.pdb) do xcopy "%%~fi" "%~1" /y >nul
::echo "Converting pdbs..."
::for /r "Dlls" %%i in (*.pdb) do ( call "%cd%%pdo2mdbPath%" "%%~i")



::ROBOCOPY "C:\Users\Manue\Work\FAtiMA-Toolkit\AffectRecognition" "C:\Users\Manue\Work\FAtiMA-Toolkit\Dlls" *.dll /S 
