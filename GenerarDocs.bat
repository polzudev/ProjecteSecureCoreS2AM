@echo off
echo Generant documentacio HTML amb Sandcastle...

set MSBUILD="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
set SHFBPROJ="%~dp0documentacioProjecte\documentacioProjecte.shfbproj"

%MSBUILD% %SHFBPROJ% /p:Configuration=Debug /nologo /v:m

if %ERRORLEVEL% == 0 (
    echo.
    echo Documentacio generada correctament!
    echo Obrint el navegador...
    start "" "%~dp0documentacioProjecte\Help\index.html"
) else (
    echo.
    echo Error en generar la documentacio. Revisa que has compilat el projecte principal primer.
    pause
)
