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

watch | Get-Item | Where-Object { $_.Extension -eq ".cs" } | % {

	Write-Output "---------Building Release------------"

	cd src && dotnet build --configuration Release -o ..\$path
	cd ..

	Write-Output "
	
Running Commands"


	Test "Running talk repeat" ".\$path\BotCli.exe talk -r I repeat everything!" "Bob: I repeat everything!" $true
	Test "Running where" ".\$path\BotCli.exe where" "C:\Users\axels\MS-Code\dotnet-tool-template\src\ReleaseBuild" $true

	Write-Output "------------Cleaning up-------------"
	rm-rf $path
	Write-Output "Passed: $Score/$Tests
Skipped: $Skipped
Total: $Total
"
}