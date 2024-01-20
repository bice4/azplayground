[CmdletBinding()]
# Parameters
param(
    [Parameter(Mandatory = $True, ValueFromPipelineByPropertyName = $True)]
    [string]
    $Subscription,

    [Parameter(Mandatory = $True, ValueFromPipelineByPropertyName = $True)]
    [ValidateNotNullOrEmpty()]
    [string] $ResourceGroupName
)

$AzModuleVersion = "7.0.0"
$ErrorActionPreference = "Stop"

# Verify that the Az module is installed 
if (!(Get-InstalledModule -Name Az -MinimumVersion $AzModuleVersion -ErrorAction SilentlyContinue)) {
    Write-Host "This script requires to have Az Module version $AzModuleVersion installed..
It was not found, please install from: https://docs.microsoft.com/en-us/powershell/azure/install-az-ps"
    exit
}

try {
    # Verify template file path
    Push-Location "$PSScriptRoot"

    # Sign in
    Write-Host "Connecting to Azure account..."
    $context = Get-AzContext -ErrorAction "SilentlyContinue"

    if (!$context -or !$context.Subscription -or !$context.SubscriptionId -eq $Subscription) {
        Connect-AzAccount
        Write-Host "Selecting subscription '$Subscription'";
        Select-AzSubscription -SubscriptionId $Subscription;
    }

    # Create or check for existing resource group
    $resourceGroup = Get-AzResourceGroup -Name $ResourceGroupName -ErrorAction SilentlyContinue
    if (!$resourceGroup) {    
        Write-Host "Resource group '$ResourceGroupName' not exists.";
        exit
    }
    else {
        Remove-AzResourceGroup -Name $ResourceGroupName -Verbose -Force
    }

    Write-Host "Specified target resource group '$ResourceGroupName' deleted successfully."
}    
catch {
    Write-Host -ForegroundColor Red $_
    exit;
}
finally {
    # Disconnect az account
    Write-Host -ForegroundColor Green "Disconnecting AzAccount for SubscriptionId $Subscription."
    Disconnect-AzAccount -ErrorAction SilentlyContinue 1>$null
}
