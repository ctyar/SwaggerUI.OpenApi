Get-ChildItem -Path '.\artifacts' | Remove-Item -Force -Recurse

dotnet pack src\SwaggerUi.OpenApi\SwaggerUi.OpenApi.csproj -o artifacts