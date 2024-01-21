[CmdletBinding()]
# Parameters
param(
    [Parameter(Mandatory = $True, ValueFromPipelineByPropertyName = $True)]
    [string]
    $Subscription,

    [Parameter(Mandatory = $True, ValueFromPipelineByPropertyName = $True)]
    [ValidateNotNullOrEmpty()]
    [string] $ResourceGroupName,

    [Parameter(Mandatory = $True, ValueFromPipelineByPropertyName = $True)]
    [ValidateSet('centralus', 'eastasia', 'southeastasia', 'eastus', 'eastus2', 'westus', `
            'westus2', 'northcentralus', 'southcentralus', 'westcentralus', 'northeurope', 'westeurope', `
            'japaneast', 'japanwest', 'brazilsouth', 'australiasoutheast', 'australiaeast', 'westindia', `
            'southindia', 'centralindia', 'canadacentral', 'canadaeast', 'uksouth', 'ukwest', 'koreacentral', `
            'koreasouth', 'francecentral', 'southafricanorth', 'uaenorth', 'australiacentral', 'switzerlandnorth', `
            'germanywestcentral', 'norwayeast', 'polandcentral')]
    [string] $ResourceGroupLocation,

    [Parameter(Mandatory = $True, ValueFromPipelineByPropertyName = $True)]
    [string] $RegistryName

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
    $TemplateFilePath = Resolve-Path "template.bicep"

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
        Write-Host "Creating resource group '$ResourceGroupName' in location '$ResourceGroupLocation'";
        $resourceGroup = New-AzResourceGroup -Name $ResourceGroupName -Location $ResourceGroupLocation    
    }
    else {
        Write-Warning "Specified target resource group '$ResourceGroupName' already exists:"
        $resourceGroup
    }

    $ResourceGroupName = $resourceGroup.ResourceGroupName

    Write-Host "Deploying Azure template..."

    # New-AzResourceGroupDeployment -Verbose -ResourceGroupName $ResourceGroupName -TemplateFile $TemplateFilePath -RegistryName $RegistryName -RegistryLocation $ResourceGroupLocation

    az deployment group create --name ExampleDeployment --resource-group $ResourceGroupName --template-file $TemplateFilePath  --parameters RegistryName=$RegistryName RegistryLocation=$ResourceGroupLocation

    Write-Host "Microsoft Azure ACR was successfully deployed."
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
