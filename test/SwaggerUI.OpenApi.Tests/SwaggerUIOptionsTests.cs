using Microsoft.AspNetCore.Builder;

namespace SwaggerUI.OpenApi.Tests;

public class SwaggerUIOptionsTests
{
    [Fact]
    public void ConfigUrl()
    {
        var expected =
            """
            "configUrl":"/configs/urls.yaml"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ConfigUrl = "/configs/urls.yaml"
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Urls()
    {
        var expected =
            """
            "urls":[{"url":"/openapi/doc1.json","name":"name1"},{"url":"/openapi/doc2.json","name":"name2"}]
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            Urls = [new UrlDescriptor {
                Name = "name1",
                Url = "/openapi/doc1.json"
            }, new UrlDescriptor{
                Name = "name2",
                Url = "/openapi/doc2.json"
            }]
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void PrimaryUrl()
    {
        var expected =
            """
            "urls.primaryName":"name2"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            Urls = [new UrlDescriptor {
                Name = "name1",
                Url = "/openapi/doc1.json"
            }, new UrlDescriptor{
                Name = "name2",
                Url = "/openapi/doc2.json"
            }],
            PrimaryUrl = "name2"
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void QueryConfigEnabled()
    {
        var expected =
            """
            "queryConfigEnabled":true
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            QueryConfigEnabled = true,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Layout()
    {
        var expected =
            """
            "layout":"BaseLayout"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            Layout = "BaseLayout",
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Plugins()
    {
        var expected =
            """
            configObject.plugins = [SwaggerUIBundle.plugins.Auth,SwaggerUIBundle.plugins.Configs];
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            Plugins = ["SwaggerUIBundle.plugins.Auth", "SwaggerUIBundle.plugins.Configs"],
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void PluginsNull()
    {
        var expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>My Document</title>
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
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            Plugins = null,
        }, []);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Presets()
    {
        var expected =
            """
            configObject.presets = [SwaggerUIStandalonePreset];
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            Presets = ["SwaggerUIStandalonePreset"],
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void DeepLinking()
    {
        var expected =
            """
            "deepLinking":true
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            DeepLinking = true,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void DisplayOperationId()
    {
        var expected =
            """
            "displayOperationId":true
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            DisplayOperationId = true,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void DefaultModelsExpandDepth()
    {
        var expected =
            """
            "defaultModelsExpandDepth":-1
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            DefaultModelsExpandDepth = -1,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void DefaultModelExpandDepth()
    {
        var expected =
            """
            "defaultModelExpandDepth":1
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            DefaultModelExpandDepth = 1,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Theory]
    [InlineData(ModelRendering.Example, "example")]
    [InlineData(ModelRendering.Model, "model")]
    public void DefaultModelRendering(ModelRendering modelRendering, string jsonValue)
    {
        var expected =
            $"""
            "defaultModelRendering":"{jsonValue}"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            DefaultModelRendering = modelRendering,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void DisplayRequestDuration()
    {
        var expected =
            """
            "displayRequestDuration":true
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            DisplayRequestDuration = true,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Theory]
    [InlineData(DocExpansion.Full, "full")]
    [InlineData(DocExpansion.List, "list")]
    [InlineData(DocExpansion.None, "none")]
    public void DocExpansionTest(DocExpansion docExpansion, string jsonValue)
    {
        var expected =
            $"""
            "docExpansion":"{jsonValue}"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            DocExpansion = docExpansion,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Filter()
    {
        var expected =
            """
            "filter":"my filter"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            Filter = "my filter",
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void MaxDisplayedTags()
    {
        var expected =
            """
            "maxDisplayedTags":5
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            MaxDisplayedTags = 5,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Theory]
    [InlineData("alpha", "\"alpha\";")]
    [InlineData("method", "\"method\";")]
    [InlineData("(a, b) => a;", "(a, b) => a;")]
    public void OperationsSorter(string operationsSorter, string result)
    {
        var expected =
            $"""
            configObject.operationsSorter = {result}
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OperationsSorter = operationsSorter,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ShowExtensions()
    {
        var expected =
            """
            "showExtensions":true
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ShowExtensions = true,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ShowCommonExtensions()
    {
        var expected =
            """
            "showCommonExtensions":false
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ShowCommonExtensions = false,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ShowCommonExtensionsNull()
    {
        var expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>My Document</title>
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
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[],"layout":"StandaloneLayout","requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

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

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ShowCommonExtensions = null,
        }, []);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("alpha", "\"alpha\";")]
    [InlineData("(a, b) => a;", "(a, b) => a;")]
    public void TagsSorter(string tagsSorter, string result)
    {
        var expected =
            $"""
            configObject.tagsSorter = {result}
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            TagsSorter = tagsSorter,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void OnComplete()
    {
        var expected =
            """
            configObject.onComplete = () => { if(window.completeCount) { window.completeCount++ } else { window.completeCount = 1 }
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OnComplete = "() => { if(window.completeCount) { window.completeCount++ } else { window.completeCount = 1 } }",
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void SyntaxHighlightActivated()
    {
        var expected =
            """
            "syntaxHighlight":{"activated":false}
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            SyntaxHighlight = new SyntaxHighlightOptions
            {
                Activated = false,
            },
        }, []);

        Assert.Contains(expected, actual);
    }

    [Theory]
    [InlineData(SyntaxHighlightThemeType.Agate, "agate")]
    [InlineData(SyntaxHighlightThemeType.Arta, "arta")]
    [InlineData(SyntaxHighlightThemeType.Monokai, "monokai")]
    [InlineData(SyntaxHighlightThemeType.Nord, "nord")]
    [InlineData(SyntaxHighlightThemeType.Obsidian, "obsidian")]
    [InlineData(SyntaxHighlightThemeType.TomorrowNight, "tomorrow-night")]
    [InlineData(SyntaxHighlightThemeType.Idea, "idea")]
    public void SyntaxHighlightOptions(SyntaxHighlightThemeType theme, string jsonValue)
    {
        var expected =
            $$"""
            "syntaxHighlight":{"theme":"{{jsonValue}}"}
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            SyntaxHighlight = new SyntaxHighlightOptions
            {
                Theme = theme,
            },
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void TryItOutEnabled()
    {
        var expected =
            """
            "tryItOutEnabled":false
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            TryItOutEnabled = false,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void RequestSnippetsEnabled()
    {
        var expected =
            """
            "requestSnippetsEnabled":false
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            RequestSnippetsEnabled = false,
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void RequestSnippetsEnabledNull()
    {
        var expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>My Document</title>
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
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[],"layout":"StandaloneLayout","showCommonExtensions":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

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

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            RequestSnippetsEnabled = null,
        }, []);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RequestSnippets()
    {
        var expected = """
            <!DOCTYPE html>
            <html lang="en">
              <head>
                <meta charset="UTF-8">
                <title>My Document</title>
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
                    var configObject = JSON.parse('{"dom_id":"#swagger-ui","urls":[],"layout":"StandaloneLayout","showCommonExtensions":true,"requestSnippetsEnabled":true,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"persistAuthorization":true}');

                    configObject.presets = [SwaggerUIBundle.presets.apis,SwaggerUIStandalonePreset];
                    configObject.plugins = [SwaggerUIBundle.plugins.DownloadUrl];
                    configObject.requestSnippets = {
                      generators: {
                        curl_powershell: {
                          title: "cURL (PowerShell)",
                          syntax: "powershell"
                        },
                        curl_bash: {
                          title: "cURL (bash)",
                          syntax: "bash"
                        },
                        curl_cmd: {
                          title: "cURL (CMD)",
                          syntax: "bash"
                        },
                      },
                      defaultExpanded: true,
                      languages: ['curl_powershell'],
                    };

                    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
                      configObject.oauth2RedirectUrl = (new URL("swagger/oauth2-redirect.html", window.location.href)).href;

                    const ui = SwaggerUIBundle(configObject);

                    window.ui = ui;
                  };
                </script>
              </body>
            </html>
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            RequestSnippets =
            """
            {
                      generators: {
                        curl_powershell: {
                          title: "cURL (PowerShell)",
                          syntax: "powershell"
                        },
                        curl_bash: {
                          title: "cURL (bash)",
                          syntax: "bash"
                        },
                        curl_cmd: {
                          title: "cURL (CMD)",
                          syntax: "bash"
                        },
                      },
                      defaultExpanded: true,
                      languages: ['curl_powershell'],
                    };
            """,
        }, []);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OAuth2RedirectUrl()
    {
        // https://github.com/swagger-api/swagger-ui/blob/master/docs/usage/configuration.md#network
        var expected =
            """
            "oauth2RedirectUrl":"https://example.com"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuth2RedirectUrl = "https://example.com"
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void RequestInterceptor()
    {
        var expected =
            """
            requestInterceptor = function (req) { req.headers['MyCustomHeader'] = 'CustomValue'; return req; }
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            RequestInterceptor = "function (req) { req.headers['MyCustomHeader'] = 'CustomValue'; return req; }"
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void RequestCurlOptions()
    {
        var expected =
            """
            "request.curlOptions":["-g","--limit-rate 20k"]
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            RequestCurlOptions = ["-g", "--limit-rate 20k"]
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ResponseInterceptor()
    {
        var expected =
            """
            responseInterceptor = function (res) { console.log(res); return res; }
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ResponseInterceptor = "function (res) { console.log(res); return res; }"
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ShowMutatedRequest()
    {
        var expected =
            """
            "showMutatedRequest":true
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ShowMutatedRequest = true
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void SupportedSubmitMethods()
    {
        var expected =
            """
            "supportedSubmitMethods":["get","post"]
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            SupportedSubmitMethods = [SubmitMethod.Get, SubmitMethod.Post]
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ValidatorUrl()
    {
        var expected =
            """
            "validatorUrl":"127.0.0.1"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ValidatorUrl = "127.0.0.1"
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void WithCredentials()
    {
        var expected =
            """
            "withCredentials":true
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            WithCredentials = true
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ModelPropertyMacro()
    {
        // https://github.com/swagger-api/swagger-ui/blob/master/docs/usage/configuration.md#macros
        var expected =
            """
            modelPropertyMacro = function (prop) { return (prop || {}).default; };
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ModelPropertyMacro = "function (prop) { return (prop || {}).default; };"
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ParameterMacro()
    {
        // https://github.com/swagger-api/swagger-ui/blob/master/docs/usage/configuration.md#macros
        var expected =
            """
            parameterMacro = function (operation, parameter) { return 'my defult value' };
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            ParameterMacro = "function (operation, parameter) { return 'my defult value' };"
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void PersistAuthorization()
    {
        // https://github.com/swagger-api/swagger-ui/blob/master/docs/usage/configuration.md#authorization
        var expected =
            """
            "persistAuthorization":false
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            PersistAuthorization = false
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void AdditionalItems()
    {
        // https://github.com/swagger-api/swagger-ui/blob/master/docs/usage/configuration.md#authorization
        var expected =
            """
            "myKey":"myValue"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            AdditionalItems = new Dictionary<string, object>
            {
                { "myKey", "myValue" }
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void PreAuthorizeBasic()
    {
        var expected =
            """
            ui.onComplete = function() { ui.preauthorizeBasic('myAuthDefinitionKey', 'myUsername', 'myPassword') };
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            PreAuthorizeBasic = new PreAuthorizeBasicOptions
            {
                AuthDefinitionKey = "myAuthDefinitionKey",
                Username = "myUsername",
                Password = "myPassword",
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void PreAuthorizeApiKey()
    {
        var expected =
            """
            ui.onComplete = function() { ui.preauthorizeApiKey('myAuthDefinitionKey', 'myApiKey') };
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            PreAuthorizeApiKey = new PreAuthorizeApiKeyOptions
            {
                AuthDefinitionKey = "myAuthDefinitionKey",
                ApiKey = "myApiKey",
            }
        }, []);

        Assert.Contains(expected, actual);
    }
}
