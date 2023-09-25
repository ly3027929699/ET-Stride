@echo off
dotnet restore
dotnet build ./DotNet/Hotfix/DotNet.Hotfix.csproj
dotnet build ./DotNet/App/DotNet.App.csproj

@echo on
pause