@echo off
@REM Makes copies of all .default files without the .default extension, only if it doesn't already exist.
@REM http://stackoverflow.com/questions/3707749/developer-specific-app-config-web-config-files-in-visual-studio/3920462#3920462

for %%f in (*.default) do (
    if not exist %%~nf (echo Copying %%~nf.default to %%~nf & copy %%~nf.default %%~nf /y)
)
echo Done.