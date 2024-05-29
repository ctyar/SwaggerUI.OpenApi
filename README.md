﻿# SwaggerUi.OpenApi

[![Build Status](https://ctyar.visualstudio.com/SwaggerUi.OpenApi/_apis/build/status%2Fctyar.SwaggerUi.OpenApi?branchName=main)](https://ctyar.visualstudio.com/SwaggerUi.OpenApi/_build/latest?definitionId=12&branchName=main)
[![SwaggerUi.OpenApi](https://img.shields.io/nuget/v/SwaggerUi.OpenApi.svg)](https://www.nuget.org/packages/SwaggerUi.OpenApi/)

A package to simplify adding Swagger UI to .NET 9's Microsoft.AspNetCore.OpenApi.


## Usage

In your `Program.cs` file Add `app.AddSwaggerUi()` and `app.MapSwaggerUi()`:

```csharp
builder.Services.AddOpenApi();
builder.Services.AddSwaggerUi();

var app = builder.Build();

app.MapOpenApi();
app.MapSwaggerUi();
```

If you want to add authentication to your Swagger you can use following helper methods:
```csharp
builder.Services.AddOpenApi("v1", o =>
{
    o.AddOAuth2(authorizationUrl, tokenUrl, scopes);
});

builder.Services.AddSwaggerUi("v1", o =>
{
    o.AddOAuth2(clientId, scopes);
});
```
There are other helper methods for Duende Identity Server `AddIdentityServer()` and Auth0 `AddAuth0`.

You can check the [samples](/src/samples) directory for complete working examples.


## Roadmap

✅ Basic UI

✅ Authentication

⏳ Enable all the documents


## Acknowledgement

This project is based on [Swashbuckle.AspNetCore.SwaggerUI](https://github.com/domaindrivendev/Swashbuckle.AspNetCore). Thanks for their awesome work.


## Pre-release builds

Get the package from [here](https://github.com/ctyar/SwaggerUi.OpenApi/pkgs/nuget/SwaggerUi.OpenApi).


## Build

[Install](https://get.dot.net) the [required](global.json) .NET SDK and run:
```
$ dotnet build
```