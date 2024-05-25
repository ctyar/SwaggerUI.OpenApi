namespace Microsoft.AspNetCore.Builder;

public sealed class SwaggerUiOptions
{
    /// <summary>
    /// Gets the JavaScript config object, represented as JSON, that will be passed to the initOAuth method
    /// </summary>
    public OAuthOptions OAuthOptions { get; set; } = new OAuthOptions();
}
