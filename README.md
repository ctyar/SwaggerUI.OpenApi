# SwaggerUI.OpenApi

[![Build Status](https://ctyar.visualstudio.com/SwaggerUI.OpenApi/_apis/build/status%2Fctyar.SwaggerUI.OpenApi?branchName=main)](https://ctyar.visualstudio.com/SwaggerUI.OpenApi/_build/latest?definitionId=13&branchName=main)
[![SwaggerUI.OpenApi](https://img.shields.io/nuget/v/SwaggerUI.OpenApi.svg)](https://www.nuget.org/packages/SwaggerUI.OpenApi/)

A package to simplify adding Swagger UI to .NET 9's Microsoft.AspNetCore.OpenApi.


## Usage

1. In your `Program.cs` file Add `app.AddSwaggerUI()` and `app.MapSwaggerUI()`:

    ```diff
      builder.Services.AddOpenApi();
    + builder.Services.AddSwaggerUI();

      var app = builder.Build();

      app.MapOpenApi();
    + app.MapSwaggerUI();
    ```

2. (Optional) Modify your `launchSettings.json` file to open Swagger automatically:
    ```diff
    -  "launchBrowser": false,
    +  "launchBrowser": true,
      "applicationUrl": "http://localhost:5150",
    +  "launchUrl": "swagger",
    ```

Please note that for .NET 10 and later, the library sets the `JsonSerializerOptions.NumberHandling` to `Strict` for better
Swagger experience.

If this is not desired, you can set it to `AllowReadingFromString` in your `Program.cs` file.
```csharp
builder.Services.AddSwaggerUI();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
});
```

## Features
### Authentication
If you want to add authentication to your Swagger you can use the following helper methods:
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
There are other helper methods for Duende Identity Server `AddIdentityServer()` and Auth0 `AddAuth0()`.

You can check the [samples](/src/samples) directory for complete working examples.

### More snippet
Adds PowerShell and CMD cURL to the request snippet by default
![Request snippet](https://raw.githubusercontent.com/ctyar/SwaggerUI.OpenApi/refs/heads/main/doc/images/snippet.png)

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
![Agate](https://raw.githubusercontent.com/ctyar/SwaggerUI.OpenApi/refs/heads/main/doc/images/agate.png)
![Arta](https://raw.githubusercontent.com/ctyar/SwaggerUI.OpenApi/refs/heads/main/doc/images/arta.png)
![Obsidian](https://raw.githubusercontent.com/ctyar/SwaggerUI.OpenApi/refs/heads/main/doc/images/obsidian.png)

### Parameter validation
Displays the common validations in the parameters form by default
```csharp
app.MapGet("/products",
    ([Range(0, 10000)] int id,
    [MinLength(3)][MaxLength(50)] string name,
    [RegularExpression("\\d\\d-\\d\\d")] string code) => id)
```
![Validation](https://raw.githubusercontent.com/ctyar/SwaggerUI.OpenApi/refs/heads/main/doc/images/validation.png)

### Authorization persistence
Persists authorization data by default and it would not be lost on browser close or refresh so you don't have to authenticate everytime.

## Roadmap

✅ Basic UI

✅ Authentication

✅ Enable all the documents

✅ Implement all Swagger UI options

✅ Improve test coverage

✅ Full data type support (TimeOnly, TimeSpan, Email)

⏳ .NET 10 and OpenAPI v3.1.1

⏳ Support for Asp.Versioning.Http package

## Acknowledgement

This project is based on [Swashbuckle.AspNetCore.SwaggerUI](https://github.com/domaindrivendev/Swashbuckle.AspNetCore). Thanks for their awesome work.


## Pre-release builds

Get the package from [here](https://github.com/ctyar/SwaggerUI.OpenApi/pkgs/nuget/SwaggerUI.OpenApi).


## Build

[Install](https://get.dot.net) the [required](global.json) .NET SDK and run:
```
$ dotnet build
```