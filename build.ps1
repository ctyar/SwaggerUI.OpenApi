Get-ChildItem -Path '.\artifacts' | Remove-Item -Force -Recurse

dotnet pack src\OpenApi.SwaggerUI\OpenApi.SwaggerUI.csproj -o artifacts