# OpenApi.SwaggerUI

[![Build Status](https://ctyar.visualstudio.com/Swashbuckle.Auth/_apis/build/status%2Fctyar.Swashbuckle.Auth?branchName=main)](https://ctyar.visualstudio.com/Swashbuckle.Auth/_build/latest?definitionId=9&branchName=main)
[![Ctyar.Swashbuckle.Auth](https://img.shields.io/nuget/v/Ctyar.Swashbuckle.Auth.svg)](https://www.nuget.org/packages/Ctyar.Swashbuckle.Auth/)

A package to simplify adding Swagger UI to .NET 9's Microsoft.AspNetCore.OpenApi.

## Usage

In your `Program.cs` file Add `app.MapSwaggerUi()` after `app.MapOpenApi()`:

```csharp
app.MapOpenApi();

app.MapSwaggerUi();
```

## Build
[Install](https://get.dot.net) the [required](global.json) .NET SDK.

Run:
```
$ dotnet build
```
