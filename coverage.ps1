dotnet-coverage collect -f xml -o coverage.xml dotnet test SwaggerUI.OpenApi.sln
reportgenerator -reports:coverage.xml -targetdir:.\report -assemblyfilters:+SwaggerUI.OpenApi.dll
.\report\index.html