param(
    [Parameter(Mandatory = $False)]
    [string] $ParametersFilePath = "deploy-parameters.json"
)


# Verify file path
Push-Location "$PSScriptRoot"
$ParametersFilePath = Resolve-Path $ParametersFilePath

$json = Get-Content $ParametersFilePath | Out-String | ConvertFrom-Json

$json | .\clean.ps1 
