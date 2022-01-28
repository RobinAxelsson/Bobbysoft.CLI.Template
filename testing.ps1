$path = "x__release"

Write-Output "---------Building Release------------"

cd src && dotnet build --configuration Release -o ..\x__release
cd ..

Write-Output "

Running Commands"

Write-Output "This is Piped" | .\x__release\BotCli.exe talk

Write-Output "------------Cleaning up-------------"
rm-rf $path