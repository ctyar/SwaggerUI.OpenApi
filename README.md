# SwaggerUi.OpenApi

[![Build Status](https://ctyar.visualstudio.com/SwaggerUi.OpenApi/_apis/build/status%2Fctyar.SwaggerUi.OpenApi?branchName=main)](https://ctyar.visualstudio.com/SwaggerUi.OpenApi/_build/latest?definitionId=12&branchName=main)
[![SwaggerUi.OpenApi](https://img.shields.io/nuget/v/SwaggerUi.OpenApi.svg)](https://www.nuget.org/packages/SwaggerUi.OpenApi/)

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
