using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SwaggerUI.OpenApi.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public async Task InexWithOneDocument()
    {
        var expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>v1</title>
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css" />
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/index.css" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-32x32.png" sizes="32x32" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-16x16.png" sizes="16x16" />
              </head>

              <body>
                <div id="swagger-ui"></div>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js" charset="UTF-8"> </script>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js" charset="UTF-8"> </script>
                <script>
                  window.onload = function() {
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[{"url":"/openapi/v1.json","name":"v1"}],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("swagger", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task IndexWithOneDocumentAndConfig()
    {
        var expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>v1</title>
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css" />
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/index.css" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-32x32.png" sizes="32x32" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-16x16.png" sizes="16x16" />
              </head>

              <body>
                <div id="swagger-ui"></div>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js" charset="UTF-8"> </script>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js" charset="UTF-8"> </script>
                <script>
                  window.onload = function() {
                    var configObject = JSON.parse('{"configUrl":"/configs/urls.yaml","dom_id":"#swagger-ui","urls":[{"url":"/openapi/v1.json","name":"v1"}],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI(o =>
        {
            o.ConfigUrl = "/configs/urls.yaml";
        });

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("swagger", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task IndexWithMultipleDocuments()
    {
        var expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>doc1</title>
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css" />
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/index.css" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-32x32.png" sizes="32x32" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-16x16.png" sizes="16x16" />
              </head>

              <body>
                <div id="swagger-ui"></div>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js" charset="UTF-8"> </script>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js" charset="UTF-8"> </script>
                <script>
                  window.onload = function() {
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[{"url":"/openapi/doc1.json","name":"doc1"},{"url":"/openapi/doc2.json","name":"doc2"}],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi("doc1");
        builder.Services.AddOpenApi("doc2");
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("swagger", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task SpecificDocumentWithOneDocument()
    {
        var expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>v1</title>
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css" />
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/index.css" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-32x32.png" sizes="32x32" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-16x16.png" sizes="16x16" />
              </head>

              <body>
                <div id="swagger-ui"></div>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js" charset="UTF-8"> </script>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js" charset="UTF-8"> </script>
                <script>
                  window.onload = function() {
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[{"url":"/openapi/v1.json","name":"v1"}],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("swagger/v1", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task SpecificDocumentWithMultipleDocuments()
    {
        var doc1Expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>doc1</title>
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css" />
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/index.css" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-32x32.png" sizes="32x32" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-16x16.png" sizes="16x16" />
              </head>

              <body>
                <div id="swagger-ui"></div>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js" charset="UTF-8"> </script>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js" charset="UTF-8"> </script>
                <script>
                  window.onload = function() {
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[{"url":"/openapi/doc1.json","name":"doc1"},{"url":"/openapi/doc2.json","name":"doc2"}],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var doc2Expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>doc2</title>
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css" />
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/index.css" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-32x32.png" sizes="32x32" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-16x16.png" sizes="16x16" />
              </head>

              <body>
                <div id="swagger-ui"></div>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js" charset="UTF-8"> </script>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js" charset="UTF-8"> </script>
                <script>
                  window.onload = function() {
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[{"url":"/openapi/doc1.json","name":"doc1"},{"url":"/openapi/doc2.json","name":"doc2"}],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi("doc1");
        builder.Services.AddOpenApi("doc2");
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var doc1Actual = await client.GetStringAsync("swagger/doc1", TestContext.Current.CancellationToken);
        var doc2Actual = await client.GetStringAsync("swagger/doc2", TestContext.Current.CancellationToken);

        Assert.Equal(doc1Expected, doc1Actual);
        Assert.Equal(doc2Expected, doc2Actual);
    }

    [Fact]
    public async Task SpecificDocumentWithMultipleDocumentsAndConfigs()
    {
        var doc1Expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>doc1</title>
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css" />
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/index.css" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-32x32.png" sizes="32x32" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-16x16.png" sizes="16x16" />
              </head>

              <body>
                <div id="swagger-ui"></div>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js" charset="UTF-8"> </script>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js" charset="UTF-8"> </script>
                <script>
                  window.onload = function() {
                    var configObject = JSON.parse('{"configUrl":"/configs/urls.yaml","dom_id":"#swagger-ui","urls":[{"url":"/openapi/doc1.json","name":"doc1"},{"url":"/openapi/doc2.json","name":"doc2"}],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var doc2Expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>doc2</title>
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/swagger-ui.css" />
                <link rel="stylesheet" type="text/css" href="https://unpkg.com/swagger-ui-dist/index.css" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-32x32.png" sizes="32x32" />
                <link rel="icon" type="image/png" href="https://unpkg.com/swagger-ui-dist/favicon-16x16.png" sizes="16x16" />
              </head>

              <body>
                <div id="swagger-ui"></div>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-bundle.js" charset="UTF-8"> </script>
                <script src="https://unpkg.com/swagger-ui-dist/swagger-ui-standalone-preset.js" charset="UTF-8"> </script>
                <script>
                  window.onload = function() {
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[{"url":"/openapi/doc1.json","name":"doc1"},{"url":"/openapi/doc2.json","name":"doc2"}],"queryConfigEnabled":true,"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi("doc1");
        builder.Services.AddOpenApi("doc2");
        builder.Services.AddSwaggerUI("doc1", o =>
        {
            o.ConfigUrl = "/configs/urls.yaml";
        });
        builder.Services.AddSwaggerUI("doc2", o =>
        {
            o.QueryConfigEnabled = true;
        });

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var doc1Actual = await client.GetStringAsync("swagger/doc1", TestContext.Current.CancellationToken);
        var doc2Actual = await client.GetStringAsync("swagger/doc2", TestContext.Current.CancellationToken);

        Assert.Equal(doc1Expected, doc1Actual);
        Assert.Equal(doc2Expected, doc2Actual);
    }

    [Fact]
    public async Task OAuth2RedirectIsLatestVersion()
    {
        using var httpClient = new HttpClient();
        var expected = await httpClient.GetStringAsync("https://raw.githubusercontent.com/swagger-api/swagger-ui/master/dist/oauth2-redirect.html", TestContext.Current.CancellationToken);
        expected = expected.Replace("\n", "\r\n");

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("swagger/oauth2-redirect.html", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }
}
