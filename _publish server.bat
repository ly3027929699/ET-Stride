@echo off
set MSBuild=msbuild.exe
MSBuild ./DotNet/Hotfix/DotNet.Hotfix.csproj
MSBuild ./DotNet/App/DotNet.App.csproj

@echo on
pause