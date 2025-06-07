using System;
using System.Web;
using SwaggerUI;

namespace Microsoft.AspNetCore.OpenApi;

public static class OpenApiOptionsExtensions
{
    /// <summary>
    /// Add Duende Identity Server OAuth 2 security definition and a global security requirement to the generated Swagger
    /// </summary>
    /// <param name="openApiOptions"></param>
    /// <param name="authority"></param>
    /// <param name="scopes">The scopes to be used for authorizationCode OAuth flow.</param>
    public static void AddIdentityServer(this OpenApiOptions openApiOptions, string authority, string[] scopes)
    {
        var authorityUrl = new Uri(authority);

        AddOAuth2(openApiOptions, new Uri(authorityUrl, "connect/authorize"), new Uri(authorityUrl, "connect/token"), scopes);
    }

    /// <summary>
    /// Add Auth0 OAuth 2 security definition and a global security requirement to the generated Swagger
    /// </summary>
    /// <param name="openApiOptions"></param>
    /// <param name="authority"></param>
    /// <param name="audience"></param>
    /// <param name="scopes">The scopes to be used for authorizationCode OAuth flow.</param>
    public static void AddAuth0(this OpenApiOptions openApiOptions, string authority, string audience, string[] scopes)
    {
        var authorityUrl = new Uri(authority);

        var httpValueCollection = HttpUtility.ParseQueryString(authorityUrl.Query);
        httpValueCollection.Add("audience", audience);

        var authorizationUrl = new UriBuilder(authorityUrl)
        {
            Query = httpValueCollection.ToString(),
            Path = "authorize"
        }.Uri;

        var tokenUrl = new UriBuilder(authorityUrl)
        {
            Path = "oauth/token"
        }.Uri;

        AddOAuth2(openApiOptions, authorizationUrl, tokenUrl, scopes);
    }

    /// <summary>
    /// Add OAuth 2 security definitions and a global security requirement to the generated Swagger
    /// </summary>
    /// <param name="openApiOptions"></param>
    /// <param name="authorizationUrl">The authorization URL to be used for authorizationCode OAuth flow.</param>
    /// <param name="tokenUrl">The token URL to be used for authorizationCode OAuth flow.</param>
    /// <param name="scopes">The scopes to be used for authorizationCode OAuth flow.</param>
    public static void AddOAuth2(this OpenApiOptions openApiOptions, Uri authorizationUrl, Uri tokenUrl, string[] scopes)
    {
        openApiOptions.AddDocumentTransformer(new SecuritySchemeTransformer(authorizationUrl, tokenUrl, scopes));
    }
}