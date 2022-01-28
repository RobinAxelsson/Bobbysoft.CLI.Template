Import-Module pswatch

$global:Score = 0
$global:Tests = 0
$global:Skipped = 0
$global:Total = 0

$path = "x__release"
function Test($title, $command, $expectedOutput, $test){
	
	Set-Variable -Name "Total" -value $($Total+1) -scope global

	Write-Output "-----------------------------"
	Write-Output "Title: $title"
	Write-Output "Is tested: $test"
	Write-Output "Input: $command"
	Write-Output "Expected output: $expectedOutput"
	$actualOutput = $(Invoke-Expression $command);
	Write-Output "Actual   output: $actualOutput"
	
	if(-not $test){
		Set-Variable -Name "Skipped" -value $($Skipped+1) -scope global
		return
	}

	Set-Variable -Name "Tests" -value $($Tests+1) -scope global
	
	if ($actualOutput -eq $expectedOutput) {
		Set-Variable -Name "Score" -value $($Score+1) -scope global
	}
}

	Write-Output "---------Building Release------------"

	cd src && dotnet build --configuration Release -o ..\$path
	cd ..

	Write-Output "
	
Running Commands"


	Test "Running talk repeat" ".\$path\BotCli.exe talk -r I repeat everything!" "Bob: I repeat everything!" $true
	Test "Running Clip" ".\$path\BotCli.exe clip" "N/A" $false
	Test "Running Clip input" ".\$path\BotCli.exe clip --input This is copied to clipoard" "N/A" $false
	Test "Running talk pipe" "Write-Output This is piped | .\$path\BotCli.exe talk" "N/A" $false
	Test "Running where" ".\$path\BotCli.exe where" "C:\Users\axels\MS-Code\dotnet-tool-template\x__release" $true
	Test "Running where userprofile" ".\$path\BotCli.exe where --name userprofile" "N/A" $false
	Test "Running where temp" ".\$path\BotCli.exe where --name temp" "N/A" $false
	Test "Running where temp" ".\$path\BotCli.exe where --name system" "N/A" $false
	Test "Running where temp" ".\$path\BotCli.exe where --name programfiles" "N/A" $false
	Test "Running where temp" ".\$path\BotCli.exe where --name desktop" "N/A" $false
	Test "Running where temp" ".\$path\BotCli.exe where --name randomstring" "N/A" $false

	Write-Output "------------Cleaning up-------------"
	rm-rf $path
	Write-Output "Passed: $Score/$Tests
Skipped: $Skipped
Total: $Total
"