using Microsoft.AspNetCore.Mvc.Testing;

namespace SwaggerUI.OpenApi.Tests.SwaggerUIServiceCollectionTests;

public class TwoDocumentsTests : IClassFixture<WebApplicationFactory<TwoDocuments.Program>>
{
    private readonly WebApplicationFactory<TwoDocuments.Program> _factory;

    public TwoDocumentsTests(WebApplicationFactory<TwoDocuments.Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task AddSwaggerUIWithTwoDocuments()
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

        var client = _factory.CreateClient();
        var actual = await client.GetStringAsync("swagger", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }
}
