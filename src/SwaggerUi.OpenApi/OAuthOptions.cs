using System.Collections.Generic;

namespace Microsoft.AspNetCore.Builder;

public sealed class OAuthOptions
{
    /// <summary>
    /// Default username for OAuth2 password flow.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Default clientId
    /// </summary>
    public string? ClientId { get; set; }

    /// <summary>
    /// Default clientSecret
    /// </summary>
    /// <remarks>Setting this exposes the client secrets in inline javascript in the swagger-ui generated html.</remarks>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// Realm query parameter (for oauth1) added to authorizationUrl and tokenUrl
    /// </summary>
    public string? Realm { get; set; }

    /// <summary>
    /// Application name, displayed in authorization popup
    /// </summary>
    public string? AppName { get; set; }

    /// <summary>
    /// Scope separator for passing scopes, encoded before calling, default value is a space (encoded value %20)
    /// </summary>
    public string ScopeSeparator { get; set; } = " ";

    /// <summary>
    /// String array of initially selected oauth scopes, default is empty array
    /// </summary>
    public IEnumerable<string> Scopes { get; set; } = [];

    /// <summary>
    /// Additional query parameters added to authorizationUrl and tokenUrl
    /// </summary>
    public Dictionary<string, string>? AdditionalQueryStringParams { get; set; }

    /// <summary>
    /// Only activated for the accessCode flow. During the authorization_code request to the tokenUrl,
    /// pass the Client Password using the HTTP Basic Authentication scheme
    /// (Authorization header with Basic base64encode(client_id + client_secret))
    /// </summary>
    public bool UseBasicAuthenticationWithAccessCodeGrant { get; set; }

    /// <summary>
    /// Only applies to authorizatonCode flows. Proof Key for Code Exchange brings enhanced security for OAuth public clients.
    /// The default is false
    /// </summary>
    public bool UsePkceWithAuthorizationCodeGrant { get; set; }
}
