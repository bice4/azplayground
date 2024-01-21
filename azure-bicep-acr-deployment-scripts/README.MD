# Deployment azure acr via bicep template

## :books: Content
* `deploy-parameters.json` - Parameters for deployment
* `deploy.ps1` - Deployment cmdlet, receive parameters needed for deployment
* `deploy-with-params.ps1` - Deployment cmdlet, reads parameters from `deploy-parameters.json`
* `template.bicep` - Actual bicep deployment template with acr resource

## :hammer: Preparation

Open `deploy-parameters.json` in any editor. Fill all necessary fields.
| Subscription | ResourceGroupName | ResourceGroupLocation | RegistryName |
| ------------ | ----------------- | --------------------- | ------------ |
| Azure subscription id| desired resource group name, or existing                   | location of resource group                       | desired registry name             |

## :running: Deploy

* Open powershell and execute `deploy-with-params.ps1`
* Cmdlet will check the Az module on machine
* Deployment script automatically select login/select subscription
* Check if resource group already exists then all resources will be deployed in the existing resource group, else create a new resource group with given name
* Run deployment to azure