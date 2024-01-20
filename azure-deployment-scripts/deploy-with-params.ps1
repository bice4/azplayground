param(
    [Parameter(Mandatory = $False)]
    [ValidateNotNullOrEmpty()]
    [string] $ParametersFilePath = "deploy-parameters.json"
)


# Verify file path
Push-Location "$PSScriptRoot"
$ParametersFilePath = Resolve-Path $ParametersFilePath

$json = Get-Content $ParametersFilePath | Out-String | ConvertFrom-Json

$json | .\deploy.ps1 
