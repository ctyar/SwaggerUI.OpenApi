namespace Microsoft.AspNetCore.Builder;

public sealed class SwaggerUiOptions
{
    /// <summary>
    /// Gets the JavaScript config object, represented as JSON, that will be passed to the initOAuth method
    /// </summary>
    public OAuthOptions OAuthOptions { get; set; } = new OAuthOptions();

    /// <summary>
    /// Set the Duende Identity Server clientId and scopes for the authorizatonCode flow with proof Key for Code Exchange.
    /// </summary>
    /// <param name="clientId">Default clientId</param>
    /// <param name="scopes">String array of initially selected OAuth scopes, default is empty array</param>
    public void UseIdentityServer(string clientId, params string[] scopes)
    {
        UseOAuth2(clientId, scopes);
    }

    /// <summary>
    /// Set the Auth0 clientId and scopes for the authorizatonCode flow with proof Key for Code Exchange.
    /// </summary>
    /// <param name="clientId">Default clientId</param>
    /// <param name="scopes">String array of initially selected OAuth scopes, default is empty array</param>
    public void UseAuth0(string clientId, params string[] scopes)
    {
        UseOAuth2(clientId, scopes);
    }

    /// <summary>
    /// Set the clientId and scopes for the authorizatonCode flow with proof Key for Code Exchange.
    /// </summary>
    /// <param name="clientId">Default clientId</param>
    /// <param name="scopes">String array of initially selected OAuth scopes, default is empty array</param>
    public void UseOAuth2(string clientId, params string[] scopes)
    {
        OAuthOptions.ClientId = clientId;

        if (scopes is not null)
        {
            OAuthOptions.Scopes = scopes;
        }

        OAuthOptions.UsePkceWithAuthorizationCodeGrant = true;
    }
}
