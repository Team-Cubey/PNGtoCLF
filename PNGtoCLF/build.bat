del build/PNGtoCLF.exe
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
mv bin\Release\netcoreapp3.1\win-x64\publish\PNGtoCLF.exe build