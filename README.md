# azplayground
Playground for azure docker/kubernetes stuff

## 🚀 How to:
* Ensure you have `Docker` installed and running.
* Login to azure, run `az login`
* Login to acr on Azure, run `az acr login --name youracrname`
* Tag your image `docker tag yourImage:tag youracrname.azurecr.io/imageName:tag`
* Push image to acr `docker push youracrname.azurecr.io/imageName:tag`
* Create WebApp on Azure with docker deployment from acr
* Update WebApp settings with OpenWeatherApiKey
