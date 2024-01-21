@description('Name of the azure container registry (must be globally unique)')
@minLength(5)
@maxLength(50)
param RegistryName string = 'playground-registry'

@description('Location for all resources.')
param RegistryLocation string = resourceGroup().location

resource Registry 'Microsoft.ContainerRegistry/registries@2023-11-01-preview' = {
  name: RegistryName
  location: RegistryLocation
  sku: {
    name: 'Basic'
    tier: 'Basic'
  }
  properties: {
    adminUserEnabled: true
    policies: {
      quarantinePolicy: {
        status: 'disabled'
      }
      trustPolicy: {
        type: 'Notary'
        status: 'disabled'
      }
      retentionPolicy: {
        days: 7
        status: 'disabled'
      }
      exportPolicy: {
        status: 'enabled'
      }
      azureADAuthenticationAsArmPolicy: {
        status: 'enabled'
      }
      softDeletePolicy: {
        retentionDays: 7
        status: 'disabled'
      }
    }
    encryption: {
      status: 'disabled'
    }
    dataEndpointEnabled: false
    publicNetworkAccess: 'Enabled'
    networkRuleBypassOptions: 'AzureServices'
    zoneRedundancy: 'Disabled'
    anonymousPullEnabled: false
    metadataSearch: 'Disabled'
  }
}

output acrLoginServer string = Registry.properties.loginServer
