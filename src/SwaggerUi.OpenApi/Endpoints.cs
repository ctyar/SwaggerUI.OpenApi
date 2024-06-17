using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SwaggerUi;

internal static class Endpoints
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false) },
    };

    public static string GetDefaultIndex(HttpContext httpContext)
    {
        var documentNames = GetDocumentNames(httpContext.RequestServices);

        var optionsMonitor = httpContext.RequestServices.GetService<IOptionsMonitor<SwaggerUiOptions>>()!;
        var swaggerUiOptions = optionsMonitor.Get(documentNames[0]);

        return GetIndexCore(documentNames[0], swaggerUiOptions);
    }

    public static string GetIndex(HttpContext httpContext, string documentName)
    {
        var optionsMonitor = httpContext.RequestServices.GetService<IOptionsMonitor<SwaggerUiOptions>>()!;

        var swaggerUiOptions = optionsMonitor.Get(documentName);

        return GetIndexCore(documentName, swaggerUiOptions);
    }

    internal static string GetIndexCore(string documentName, SwaggerUiOptions swaggerUiOptions)
    {
        var result = new StringBuilder(GetIndexStart(documentName));

        AppendOption(result, $"var configObject = JSON.parse('" +
            $"{JsonSerializer.Serialize(swaggerUiOptions, JsonSerializerOptions)}');\r\n");

        AppendOption(result, $"configObject.presets = [{string.Join(",", swaggerUiOptions.Presets)}];");

        if (swaggerUiOptions.Plugins is not null)
        {
            AppendOption(result, $"configObject.plugins = [{string.Join(",", swaggerUiOptions.Plugins)}];");
        }

        if (swaggerUiOptions.OperationsSorter is not null)
        {
            if (swaggerUiOptions.OperationsSorter == "alpha")
            {
                AppendOption(result, $"configObject.operationsSorter = \"alpha\";");
            }
            else if (swaggerUiOptions.OperationsSorter == "method")
            {
                AppendOption(result, $"configObject.operationsSorter = \"method\";");
            }
            else
            {
                AppendOption(result, $"configObject.operationsSorter = {swaggerUiOptions.OperationsSorter}");
            }
        }

        if (swaggerUiOptions.TagsSorter is not null)
        {
            if (swaggerUiOptions.TagsSorter == "alpha")
            {
                AppendOption(result, $"configObject.tagsSorter = \"alpha\";");
            }
            else
            {
                AppendOption(result, $"configObject.tagsSorter = {swaggerUiOptions.TagsSorter}");
            }
        }

        if (swaggerUiOptions.OnComplete is not null)
        {
            AppendOption(result, $"configObject.onComplete = {swaggerUiOptions.OnComplete}");
        }

        if (swaggerUiOptions.RequestSnippets is not null)
        {
            AppendOption(result, $"configObject.requestSnippets = {swaggerUiOptions.RequestSnippets}");
        }

        if (swaggerUiOptions.RequestInterceptor is not null)
        {
            AppendOption(result, $"configObject.requestInterceptor = {swaggerUiOptions.RequestInterceptor}");
        }

        if (swaggerUiOptions.ResponseInterceptor is not null)
        {
            AppendOption(result, $"configObject.responseInterceptor = {swaggerUiOptions.ResponseInterceptor}");
        }

        if (swaggerUiOptions.ModelPropertyMacro is not null)
        {
            AppendOption(result, $"configObject.modelPropertyMacro = {swaggerUiOptions.ModelPropertyMacro}");
        }

        if (swaggerUiOptions.ParameterMacro is not null)
        {
            AppendOption(result, $"configObject.parameterMacro = {swaggerUiOptions.ParameterMacro}");
        }

        result.Append("\r\n");
        AppendOption(result, "if (!configObject.hasOwnProperty(\"oauth2RedirectUrl\"))");
        AppendOption(result, "  configObject.oauth2RedirectUrl = (new URL(\"swagger/oauth2-redirect.html\", window.location.href)).href;\r\n");

        AppendOption(result, "const ui = SwaggerUIBundle(configObject);");

        if (swaggerUiOptions.OAuthOptions is not null)
        {
            var oauthValue = $"""
                var oauthConfigObject = JSON.parse('{JsonSerializer.Serialize(swaggerUiOptions.OAuthOptions, JsonSerializerOptions)}');
                ui.initOAuth(oauthConfigObject);
                """;

            AppendOption(result, oauthValue);
        }

        if (swaggerUiOptions.PreAuthorizeBasic is not null)
        {
            var preAuthorizeValue = $$"""
                ui.onComplete = function() { ui.preauthorizeBasic(
                  "{{swaggerUiOptions.PreAuthorizeBasic.AuthDefinitionKey}}",
                  "{{swaggerUiOptions.PreAuthorizeBasic.Username}}",
                  "{{swaggerUiOptions.PreAuthorizeBasic.Password}}");
                }
                """;

            AppendOption(result, preAuthorizeValue);
        }

        if (swaggerUiOptions.PreAuthorizeApiKey is not null)
        {
            var preAuthorizeApiKeyValue = $$"""
                ui.onComplete = function() { ui.preauthorizeApiKey(
                  "{{swaggerUiOptions.PreAuthorizeApiKey.AuthDefinitionKey}}",
                  "{{swaggerUiOptions.PreAuthorizeApiKey.ApiKey}}");
                }
                """;

            AppendOption(result, preAuthorizeApiKeyValue);
        }

        result.Append(GetIndexEnd());

        return result.ToString();
    }

    private static void AppendOption(StringBuilder stringBuilder, string value)
    {
        stringBuilder.AppendFormat("      {0}\r\n", value);
    }

    private static string GetIndexStart(string documentName)
    {
        return $$"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
          <meta charset="UTF-8">
          <title>{{documentName}}</title>
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

        """;
    }

    private static string GetIndexEnd()
    {
        return """

              window.ui = ui;
            };
          </script>
        </body>
        </html>
        """;
    }

    public static string GetOAuthRedirectHtml()
    {
        return """
        <!doctype html>
        <html lang="en-US">
        <head>
            <title>Swagger UI: OAuth2 Redirect</title>
        </head>
        <body>
        <script>
            'use strict';
            function run () {
                var oauth2 = window.opener.swaggerUIRedirectOauth2;
                var sentState = oauth2.state;
                var redirectUrl = oauth2.redirectUrl;
                var isValid, qp, arr;

                if (/code|token|error/.test(window.location.hash)) {
                    qp = window.location.hash.substring(1).replace('?', '&');
                } else {
                    qp = location.search.substring(1);
                }

                arr = qp.split("&");
                arr.forEach(function (v,i,_arr) { _arr[i] = '"' + v.replace('=', '":"') + '"';});
                qp = qp ? JSON.parse('{' + arr.join() + '}',
                        function (key, value) {
                            return key === "" ? value : decodeURIComponent(value);
                        }
                ) : {};

                isValid = qp.state === sentState;

                if ((
                  oauth2.auth.schema.get("flow") === "accessCode" ||
                  oauth2.auth.schema.get("flow") === "authorizationCode" ||
                  oauth2.auth.schema.get("flow") === "authorization_code"
                ) && !oauth2.auth.code) {
                    if (!isValid) {
                        oauth2.errCb({
                            authId: oauth2.auth.name,
                            source: "auth",
                            level: "warning",
                            message: "Authorization may be unsafe, passed state was changed in server. The passed state wasn't returned from auth server."
                        });
                    }

                    if (qp.code) {
                        delete oauth2.state;
                        oauth2.auth.code = qp.code;
                        oauth2.callback({auth: oauth2.auth, redirectUrl: redirectUrl});
                    } else {
                        let oauthErrorMsg;
                        if (qp.error) {
                            oauthErrorMsg = "["+qp.error+"]: " +
                                (qp.error_description ? qp.error_description+ ". " : "no accessCode received from the server. ") +
                                (qp.error_uri ? "More info: "+qp.error_uri : "");
                        }

                        oauth2.errCb({
                            authId: oauth2.auth.name,
                            source: "auth",
                            level: "error",
                            message: oauthErrorMsg || "[Authorization failed]: no accessCode received from the server."
                        });
                    }
                } else {
                    oauth2.callback({auth: oauth2.auth, token: qp, isValid: isValid, redirectUrl: redirectUrl});
                }
                window.close();
            }

            if (document.readyState !== 'loading') {
                run();
            } else {
                document.addEventListener('DOMContentLoaded', function () {
                    run();
                });
            }
        </script>
        </body>
        </html>

        """;
    }

    private static List<string> GetDocumentNames(IServiceProvider serviceProvider)
    {
        // https://github.com/dotnet/runtime/issues/100105
        // https://github.com/dotnet/aspnetcore/blob/3117946082a9c456f50e70075403bb024f9e323b/src/OpenApi/src/Services/OpenApiDocumentProvider.cs#L51
        var type = typeof(OpenApiOptions).Assembly.GetType("Microsoft.Extensions.ApiDescriptions.OpenApiDocumentProvider")!;
        var ctor = type.GetConstructor([typeof(IServiceProvider)])!;
        var openApiDocumentProvider = ctor.Invoke([serviceProvider]);

        var method = type.GetMethod("GetDocumentNames")!;
        var documentNames = (IEnumerable<string>)method.Invoke(openApiDocumentProvider, [])!;
        return documentNames.ToList();
    }
}
