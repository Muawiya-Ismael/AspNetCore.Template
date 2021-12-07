@echo off
cd tools/MvcTemplate.Rename
dotnet restore
cd ../..
dotnet run --pproject tools/MvcTemplate.Rename/MvcTemplate.Rename.csproj

if exist tools ( rmdir /s /q tools )
if exist README.md ( del README.md )
del /s Rename.cmd