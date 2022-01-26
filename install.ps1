cd src
dotnet pack
dotnet tool install --tool-path $HOME\.dotnet\tools --add-source ./nupkg dotnet-tool-info