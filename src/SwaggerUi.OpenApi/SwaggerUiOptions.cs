using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SwaggerUi;

namespace Microsoft.AspNetCore.Builder;

public sealed class SwaggerUiOptions
{
    /// <summary>
    /// Gets the JavaScript config object, represented as JSON, that will be passed to the initOAuth method
    /// </summary>
    [JsonIgnore]
    public OAuthOptions OAuthOptions { get; set; } = new OAuthOptions();

    /// <summary>
    /// One or more Swagger JSON endpoints (url and name) to power the UI
    /// </summary>
    public IEnumerable<UrlDescriptor> Urls { get; set; } = [];

    /// <summary>
    /// If set to true, enables deep linking for tags and operations
    /// </summary>
    public bool DeepLinking { get; set; }

    /// <summary>
    /// If set to true, it persists authorization data and it would not be lost on browser close/refresh
    /// </summary>
    public bool PersistAuthorization { get; set; }

    /// <summary>
    /// Controls the display of operationId in operations list
    /// </summary>
    public bool DisplayOperationId { get; set; }

    /// <summary>
    /// The default expansion depth for models (set to -1 completely hide the models)
    /// </summary>
    public int DefaultModelsExpandDepth { get; set; } = 1;

    /// <summary>
    /// The default expansion depth for the model on the model-example section
    /// </summary>
    public int DefaultModelExpandDepth { get; set; } = 1;

    /// <summary>
    /// Controls how the model is shown when the API is first rendered.
    /// (The user can always switch the rendering for a given model by clicking the 'Model' and 'Example Value' links)
    /// </summary>
    [JsonConverter(typeof(JavascriptStringEnumConverter<ModelRendering>))]
    public ModelRendering DefaultModelRendering { get; set; } = ModelRendering.Example;

    /// <summary>
    /// Controls the display of the request duration (in milliseconds) for Try-It-Out requests
    /// </summary>
    public bool DisplayRequestDuration { get; set; } = false;

    /// <summary>
    /// Controls the default expansion setting for the operations and tags.
    /// It can be 'list' (expands only the tags), 'full' (expands the tags and operations) or 'none' (expands nothing)
    /// </summary>
    [JsonConverter(typeof(JavascriptStringEnumConverter<DocExpansion>))]
    public DocExpansion DocExpansion { get; set; } = DocExpansion.List;

    /// <summary>
    /// If set, enables filtering. The top bar will show an edit box that you can use to filter the tagged operations
    /// that are shown. Can be an empty string or specific value, in which case filtering will be enabled using that
    /// value as the filter expression. Filtering is case sensitive matching the filter expression anywhere inside the tag
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// If set, limits the number of tagged operations displayed to at most this many. The default is to show all operations
    /// </summary>
    public int? MaxDisplayedTags { get; set; }

    /// <summary>
    /// Controls the display of vendor extension (x-) fields and values for Operations, Parameters, and Schema
    /// </summary>
    public bool ShowExtensions { get; set; }

    /// <summary>
    /// Controls the display of extensions (pattern, maxLength, minLength, maximum, minimum) fields and values for Parameters
    /// </summary>
    public bool ShowCommonExtensions { get; set; }

    /// <summary>
    /// OAuth redirect URL
    /// </summary>
    [JsonPropertyName("oauth2RedirectUrl")]
    public string? OAuth2RedirectUrl { get; set; }

    /// <summary>
    /// List of HTTP methods that have the Try it out feature enabled.
    /// An empty array disables Try it out for all operations. This does not filter the operations from the display
    /// </summary>
    [JsonConverter(typeof(JavascriptStringEnumEnumerableConverter<SubmitMethod>))]
    public IEnumerable<SubmitMethod> SupportedSubmitMethods { get; set; } = Enum.GetValues<SubmitMethod>();

    /// <summary>
    /// Controls whether the "Try it out" section should be enabled by default.
    /// </summary>
    [JsonPropertyName("tryItOutEnabled")]
    public bool TryItOutEnabled { get; set; }

    /// <summary>
    /// By default, Swagger-UI attempts to validate specs against swagger.io's online validator.
    /// You can use this parameter to set a different validator URL, for example for locally deployed validators (Validator Badge).
    /// Setting it to null will disable validation
    /// </summary>
    public string? ValidatorUrl { get; set; }

    [JsonExtensionData]
    public Dictionary<string, object> AdditionalItems { get; set; } = [];

    /// <summary>
    /// Set the Duende Identity Server clientId and scopes for the authorizatonCode flow with proof Key for Code Exchange.
    /// </summary>
    /// <param name="clientId">Default clientId</param>
    /// <param name="scopes">String array of initially selected OAuth scopes, default is empty array</param>
    public void AddIdentityServer(string clientId, params string[] scopes)
    {
        AddOAuth2(clientId, scopes);
    }

    /// <summary>
    /// Set the Auth0 clientId and scopes for the authorizatonCode flow with proof Key for Code Exchange.
    /// </summary>
    /// <param name="clientId">Default clientId</param>
    /// <param name="scopes">String array of initially selected OAuth scopes, default is empty array</param>
    public void AddAuth0(string clientId, params string[] scopes)
    {
        AddOAuth2(clientId, scopes);
    }

    /// <summary>
    /// Set the clientId and scopes for the authorizatonCode flow with proof Key for Code Exchange.
    /// </summary>
    /// <param name="clientId">Default clientId</param>
    /// <param name="scopes">String array of initially selected OAuth scopes, default is empty array</param>
    public void AddOAuth2(string clientId, params string[] scopes)
    {
        OAuthOptions.ClientId = clientId;

        if (scopes is not null)
        {
            OAuthOptions.Scopes = scopes;
        }

        OAuthOptions.UsePkceWithAuthorizationCodeGrant = true;
    }
}

public sealed class UrlDescriptor
{
    public string Url { get; set; } = null!;

    public string Name { get; set; } = null!;
}

public enum ModelRendering
{
    Example,
    Model
}

public enum DocExpansion
{
    List,
    Full,
    None
}

public enum SubmitMethod
{
    Get,
    Put,
    Post,
    Delete,
    Options,
    Head,
    Patch,
    Trace
}
