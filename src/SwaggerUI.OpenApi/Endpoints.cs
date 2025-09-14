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

namespace SwaggerUI;

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

        var optionsMonitor = httpContext.RequestServices.GetService<IOptionsMonitor<SwaggerUIOptions>>()!;
        var swaggerUIOptions = optionsMonitor.Get(documentNames[0]);

        return GetIndexCore(documentNames[0], swaggerUIOptions, documentNames);
    }

    public static string GetIndex(HttpContext httpContext, string documentName)
    {
        var documentNames = GetDocumentNames(httpContext.RequestServices);

        var optionsMonitor = httpContext.RequestServices.GetService<IOptionsMonitor<SwaggerUIOptions>>()!;

        var swaggerUIOptions = optionsMonitor.Get(documentName);

        return GetIndexCore(documentName, swaggerUIOptions, documentNames);
    }

    internal static string GetIndexCore(string documentName, SwaggerUIOptions swaggerUIOptions, List<string> documentNames)
    {
        if (!swaggerUIOptions.Urls.Any())
        {
            swaggerUIOptions.Urls = [.. documentNames.Select(d => new UrlDescriptor { Name = d, Url = $"/openapi/{d}.json" })];
        }

        var result = new StringBuilder(GetIndexStart(documentName));

        AppendOption(result, $"var configObject = JSON.parse('" +
            $"{JsonSerializer.Serialize(swaggerUIOptions, JsonSerializerOptions)}');\r\n");

        AppendOption(result, $"configObject.presets = [{string.Join(",", swaggerUIOptions.Presets)}];");

        if (swaggerUIOptions.Plugins is not null)
        {
            AppendOption(result, $"configObject.plugins = [{string.Join(",", swaggerUIOptions.Plugins)}];");
        }

        if (swaggerUIOptions.OperationsSorter is not null)
        {
            if (swaggerUIOptions.OperationsSorter == "alpha")
            {
                AppendOption(result, "configObject.operationsSorter = \"alpha\";");
            }
            else if (swaggerUIOptions.OperationsSorter == "method")
            {
                AppendOption(result, "configObject.operationsSorter = \"method\";");
            }
            else
            {
                AppendOption(result, $"configObject.operationsSorter = {swaggerUIOptions.OperationsSorter}");
            }
        }

        if (swaggerUIOptions.TagsSorter is not null)
        {
            if (swaggerUIOptions.TagsSorter == "alpha")
            {
                AppendOption(result, "configObject.tagsSorter = \"alpha\";");
            }
            else
            {
                AppendOption(result, $"configObject.tagsSorter = {swaggerUIOptions.TagsSorter}");
            }
        }

        if (swaggerUIOptions.OnComplete is not null)
        {
            AppendOption(result, $"configObject.onComplete = {swaggerUIOptions.OnComplete}");
        }

        if (swaggerUIOptions.RequestSnippets is not null)
        {
            AppendOption(result, $"configObject.requestSnippets = {swaggerUIOptions.RequestSnippets}");
        }

        if (swaggerUIOptions.RequestInterceptor is not null)
        {
            AppendOption(result, $"configObject.requestInterceptor = {swaggerUIOptions.RequestInterceptor}");
        }

        if (swaggerUIOptions.ResponseInterceptor is not null)
        {
            AppendOption(result, $"configObject.responseInterceptor = {swaggerUIOptions.ResponseInterceptor}");
        }

        if (swaggerUIOptions.ModelPropertyMacro is not null)
        {
            AppendOption(result, $"configObject.modelPropertyMacro = {swaggerUIOptions.ModelPropertyMacro}");
        }

        if (swaggerUIOptions.ParameterMacro is not null)
        {
            AppendOption(result, $"configObject.parameterMacro = {swaggerUIOptions.ParameterMacro}");
        }

        result.Append("\r\n");
        AppendOption(result, "if (!configObject.hasOwnProperty(\"oauth2RedirectUrl\"))");
        AppendOption(result, "  configObject.oauth2RedirectUrl = (new URL(\"swagger/oauth2-redirect.html\", window.location.href)).href;\r\n");

        AppendOption(result, "const ui = SwaggerUIBundle(configObject);");

        if (swaggerUIOptions.OAuthOptions is not null)
        {
            var oauthValue =
                $"var oauthConfigObject = JSON.parse('{JsonSerializer.Serialize(swaggerUIOptions.OAuthOptions, JsonSerializerOptions)}');";

            AppendOption(result, oauthValue);
            AppendOption(result, "ui.initOAuth(oauthConfigObject);");
        }

        if (swaggerUIOptions.PreAuthorizeBasic is not null)
        {
            AppendOption(result, $"ui.onComplete = function() {{ ui.preauthorizeBasic(" +
                $"'{swaggerUIOptions.PreAuthorizeBasic.AuthDefinitionKey}', " +
                $"'{swaggerUIOptions.PreAuthorizeBasic.Username}', " +
                $"'{swaggerUIOptions.PreAuthorizeBasic.Password}') }};");
        }

        if (swaggerUIOptions.PreAuthorizeApiKey is not null)
        {
            AppendOption(result, $"ui.onComplete = function() {{ ui.preauthorizeApiKey(" +
                $"'{swaggerUIOptions.PreAuthorizeApiKey.AuthDefinitionKey}', " +
                $"'{swaggerUIOptions.PreAuthorizeApiKey.ApiKey}') }};");
        }

        result.Append(GetIndexEnd());

        return result.ToString();
    }

    private static void AppendOption(StringBuilder stringBuilder, string value)
    {
        stringBuilder.Append($"        {value}\r\n");
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
            <body>
            </body>
            </html>
            <script src="oauth2-redirect.js"></script>
            """;
    }

    public static string GetOAuthRedirectJs()
    {
        return """
            "use strict";function run(){var e,r,t,a=window.opener.swaggerUIRedirectOauth2,o=a.state,n=a.redirectUrl;if((t=(r=/code|token|error/.test(window.location.hash)?window.location.hash.substring(1).replace("?","&"):location.search.substring(1)).split("&")).forEach((function(e,r,t){t[r]='"'+e.replace("=",'":"')+'"'})),e=(r=r?JSON.parse("{"+t.join()+"}",(function(e,r){return""===e?r:decodeURIComponent(r)})):{}).state===o,"accessCode"!==a.auth.schema.get("flow")&&"authorizationCode"!==a.auth.schema.get("flow")&&"authorization_code"!==a.auth.schema.get("flow")||a.auth.code)a.callback({auth:a.auth,token:r,isValid:e,redirectUrl:n});else if(e||a.errCb({authId:a.auth.name,source:"auth",level:"warning",message:"Authorization may be unsafe, passed state was changed in server Passed state wasn't returned from auth server"}),r.code)delete a.state,a.auth.code=r.code,a.callback({auth:a.auth,redirectUrl:n});else{let e;r.error&&(e="["+r.error+"]: "+(r.error_description?r.error_description+". ":"no accessCode received from the server. ")+(r.error_uri?"More info: "+r.error_uri:"")),a.errCb({authId:a.auth.name,source:"auth",level:"error",message:e||"[Authorization failed]: no accessCode received from the server"})}window.close()}"loading"!==document.readyState?run():document.addEventListener("DOMContentLoaded",(function(){run()}));
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