# azplayground [![Build and Deploy to Azure Container Registry](https://github.com/bice4/azplayground/actions/workflows/main.yml/badge.svg)](https://github.com/bice4/azplayground/actions/workflows/main.yml)
Playground for azure docker/kubernetes stuff

## 🚀 How to:
* Ensure you have `Docker` installed and running.
* Login to azure, run `az login`
* Login to acr on Azure, run `az acr login --name youracrname`
* Tag your image `docker tag yourImage:tag youracrname.azurecr.io/imageName:tag`
* Push image to acr `docker push youracrname.azurecr.io/imageName:tag`
* Create WebApp on Azure with docker deployment from acr
* Update WebApp settings with OpenWeatherApiKey

## Github actions setup
* Obtain resource group id, run ` az group show --name <resource-group> --query id`
* Create credentials **resourceGroupId** from prev step , run `az ad sp create-for-rbac --scope <resourceGroupId> --role Contributor --json-auth` save json file
* Obtain acr id of acr, run `az acr show --name <youracrname> --resource-group <resource-group> --query id` 
* Create role assignment, run `az role assignment create --assignee <clientId from json> --scope <acrId> --role AcrPush`
* Save credentials to GitHub repo


| AZURE_CREDENTIALS     | The entire JSON output from the service principal creation step                        |
|-----------------------|----------------------------------------------------------------------------------------|
| REGISTRY_LOGIN_SERVER | The login server name of your registry (all lowercase). Example: myregistry.azurecr.io |
| REGISTRY_USERNAME     | The clientId from the JSON output from the service principal creation                  |
| REGISTRY_PASSWORD     | The clientSecret from the JSON output from the service principal creation              |
| RESOURCE_GROUP        | The name of the resource group you used to scope the service principal                 |

Full guide <a href="https://learn.microsoft.com/en-us/azure/container-instances/container-instances-github-action?tabs=userlevel&tryIt=true&source=docs#code-try-0">Link</a>