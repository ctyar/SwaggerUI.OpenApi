# SwaggerUI.OpenApi

[![Build Status](https://ctyar.visualstudio.com/SwaggerUI.OpenApi/_apis/build/status%2Fctyar.SwaggerUI.OpenApi?branchName=main)](https://ctyar.visualstudio.com/SwaggerUI.OpenApi/_build/latest?definitionId=12&branchName=main)
[![SwaggerUI.OpenApi](https://img.shields.io/nuget/v/SwaggerUI.OpenApi.svg)](https://www.nuget.org/packages/SwaggerUI.OpenApi/)

A package to simplify adding Swagger UI to .NET 9's Microsoft.AspNetCore.OpenApi.


## Usage

In your `Program.cs` file Add `app.AddSwaggerUI()` and `app.MapSwaggerUI()`:

```csharp
builder.Services.AddOpenApi();
builder.Services.AddSwaggerUI();

var app = builder.Build();

app.MapOpenApi();
app.MapSwaggerUI();
```

If you want to add authentication to your Swagger you can use following helper methods:
```csharp
builder.Services.AddOpenApi("v1", o =>
{
    o.AddOAuth2(authorizationUrl, tokenUrl, scopes);
});

builder.Services.AddSwaggerUI("v1", o =>
{
    o.AddOAuth2(clientId, scopes);
});
```
There are other helper methods for Duende Identity Server `AddIdentityServer()` and Auth0 `AddAuth0`.

You can check the [samples](/src/samples) directory for complete working examples.

## Features
### More snippet
Adds PowerShell and CMD cURL to the request snippet by default
![Request snippet](https://github.com/ctyar/SwaggerUI.OpenApi/assets/1432648/34677d70-0720-4853-98d3-efa793f10b07)

### Styles
Easily change syntax highlighting style
```csharp
builder.Services.AddSwaggerUI(o =>
{
    o.SyntaxHighlight = new SyntaxHighlightOptions
    {
        Theme = SyntaxHighlightThemeType.Arta
    };
});
```
![Agate](https://github.com/ctyar/SwaggerUI.OpenApi/assets/1432648/7b8b0739-c85f-4ec9-b82b-b269b52cb373)
![Arta](https://github.com/ctyar/SwaggerUI.OpenApi/assets/1432648/bd2289d4-d21b-4214-a707-8c6852e7f663)
![Obsidian](https://github.com/ctyar/SwaggerUI.OpenApi/assets/1432648/0e9e39f6-c4b7-4599-ad13-5c4b70fbdc13)

### Parameter validation
Displays the common validations in the parameters form by default
```csharp
app.MapGet("/products",
    ([Range(0, 10000)] int id,
    [MinLength(3)][MaxLength(50)] string name,
    [RegularExpression("\\d\\d-\\d\\d")] string code) => id)
```
![Validation](https://github.com/ctyar/SwaggerUI.OpenApi/assets/1432648/4d6e16a2-52d9-4265-9054-cde9542ed820)

### Authorization persistence
Persists authorization data by default and it would not be lost on browser close or refresh so you don't have to authenticate everytime.

## Roadmap

✅ Basic UI

✅ Authentication

✅ Enable all the documents

✅ Implement all Swagger UI options

⏳ Improve test coverage

## Acknowledgement

This project is based on [Swashbuckle.AspNetCore.SwaggerUI](https://github.com/domaindrivendev/Swashbuckle.AspNetCore). Thanks for their awesome work.


## Pre-release builds

Get the package from [here](https://github.com/ctyar/SwaggerUI.OpenApi/pkgs/nuget/SwaggerUI.OpenApi).


## Build

[Install](https://get.dot.net) the [required](global.json) .NET SDK and run:
```
$ dotnet build
```