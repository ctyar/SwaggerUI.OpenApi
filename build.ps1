Get-ChildItem -Path '.\artifacts' | Remove-Item -Force -Recurse

dotnet pack src\SwaggerUI.OpenApi\SwaggerUI.OpenApi.csproj -o artifacts
dotnet pack src\SwaggerUI.OpenApi.Versioning\SwaggerUI.OpenApi.Versioning.csproj -o artifacts