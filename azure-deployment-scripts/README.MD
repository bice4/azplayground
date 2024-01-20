# Deployment azure acr via ARM template

## Content
* `deploy-parameters.json` - Parameters for deployment.
* `deploy.ps1` - Deployment cmdlet, receive parameters needed for deployment
* `deploy-with-params.ps1` - Deployment cmdlet, reads parameters from `deploy-parameters.json`
* `clean.ps1` - Cmdlet to clean up (remove resource group) receive parameters
* `clean-with-params.ps1` - Clean cmdlet read parameters from `deploy-parameters.json`
* `template.json` - Actual deployment template for ACR resource

## Preparation

Open `deploy-parameters.json` in any editor. Fill all necessary fields.
| Subscription | ResourceGroupName | ResourceGroupLocation | RegistryName |
| ------------ | ----------------- | --------------------- | ------------ |
| Azure subscription id| desired resource group name, or existing                   | location of resource group                       | desired registry name             |

## Deploy

* Open powershell and execute `deploy-with-params.ps1`
* Cmdlet will check the Az module on machine
* Deployment script automatically select login/select subscription
* Check if resource group already exists then all resources will be deployed in the existing resource group, else create a new resource group with given name
* Run deployment to azure

## Clean up

* Open powershell and execute `clean-with-params.ps1`
* Clean script automatically select login/select subscription
* Check if resource group exists and delete it **all resources in the resource group will be deleted** 