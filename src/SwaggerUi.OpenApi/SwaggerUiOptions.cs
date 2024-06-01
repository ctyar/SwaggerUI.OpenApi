using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// https://github.com/swagger-api/swagger-ui/blob/HEAD/docs/usage/configuration.md
/// </summary>
public sealed class SwaggerUiOptions
{
    /// <summary>
    /// URL to fetch external configuration document from.
    /// </summary>
    public string? ConfigUrl { get; set; }

    [JsonInclude]
    [JsonPropertyName("dom_id")]
    internal string DomId { get; private set; } = "#swagger-ui";

    /// <summary>
    /// One or more API definitions used by Topbar plugin.
    /// Names and URLs must be unique among all items, since they're used as identifiers.
    /// </summary>
    public IEnumerable<UrlDescriptor> Urls { get; set; } = [];

    /// <summary>
    /// If the value matches the name of a spec provided in <see cref="Urls"/>, that spec will be displayed when Swagger UI loads,
    /// instead of defaulting to the first spec in <see cref="Urls"/>.
    /// </summary>
    [JsonPropertyName("urls.primaryName")]
    public string? PrimaryUrl { get; set; }

    /// <summary>
    /// Enables overriding configuration parameters via URL search params.
    /// </summary>
    public bool? QueryConfigEnabled { get; set; }

    /// <summary>
    /// The name of a component available via the plugin system to use as the top-level layout for Swagger UI.
    /// </summary>
    public string Layout { get; set; } = "StandaloneLayout";

    /// <summary>
    /// Plugin functions to use in Swagger UI.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string>? Plugins { get; set; } = ["SwaggerUIBundle.plugins.DownloadUrl"];

    /// <summary>
    /// Presets to use in Swagger UI. Usually, you'll want to include ApisPreset if you use this option.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> Presets { get; set; } = ["SwaggerUIBundle.presets.apis", "SwaggerUIStandalonePreset"];

    /// <summary>
    /// If set to true, enables deep linking for tags and operations.
    /// See the <see href="https://github.com/swagger-api/swagger-ui/blob/master/docs/usage/deep-linking.md">Deep Linking
    /// documentation</see> for more information.
    /// </summary>
    public bool? DeepLinking { get; set; }

    /// <summary>
    /// Controls the display of operationId in operations list.
    /// </summary>
    public bool? DisplayOperationId { get; set; }

    /// <summary>
    /// The default expansion depth for models (set to -1 completely hide the models).
    /// </summary>
    public int? DefaultModelsExpandDepth { get; set; }

    /// <summary>
    /// The default expansion depth for the model on the model-example section.
    /// </summary>
    public int? DefaultModelExpandDepth { get; set; }

    /// <summary>
    /// Controls how the model is shown when the API is first rendered.
    /// (The user can always switch the rendering for a given model by clicking the 'Model' and 'Example Value' links.)
    /// </summary>
    public ModelRendering? DefaultModelRendering { get; set; }

    /// <summary>
    /// Controls the display of the request duration (in milliseconds) for Try-It-Out requests.
    /// </summary>
    public bool? DisplayRequestDuration { get; set; }

    /// <summary>
    /// Controls the default expansion setting for the operations and tags.
    /// It can be 'list' (expands only the tags), 'full' (expands the tags and operations) or 'none' (expands nothing).
    /// </summary>
    public DocExpansion? DocExpansion { get; set; }

    /// <summary>
    /// If set, enables filtering. The top bar will show an edit box that you can use to filter the tagged operations
    /// that are shown. Can be an empty string or specific value, in which case filtering will be enabled using that
    /// value as the filter expression. Filtering is case sensitive matching the filter expression anywhere inside the tag.
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// If set, limits the number of tagged operations displayed to at most this many. The default is to show all operations.
    /// </summary>
    public int? MaxDisplayedTags { get; set; }

    /// <summary>
    /// Apply a sort to the operation list of each API. It can be 'alpha' (sort by paths alphanumerically),
    /// 'method' (sort by HTTP method) or a function (see Array.prototype.sort() to know how sort function works).
    /// Default is the order returned by the server unchanged.
    /// </summary>
    [JsonIgnore]
    public string? OperationsSorter { get; set; }

    /// <summary>
    /// Controls the display of vendor extension (x-) fields and values for Operations, Parameters, and Schema.
    /// </summary>
    public bool? ShowExtensions { get; set; }

    /// <summary>
    /// Controls the display of extensions (pattern, maxLength, minLength, maximum, minimum) fields and values for Parameters.
    /// </summary>
    public bool? ShowCommonExtensions { get; set; }

    /// <summary>
    /// Apply a sort to the tag list of each API. It can be 'alpha' (sort by paths alphanumerically) or a function
    /// (see Array.prototype.sort() to learn how to write a sort function). Two tag name strings are passed to the sorter
    /// for each pass. Default is the order determined by Swagger UI.
    /// </summary>
    [JsonIgnore]
    public string? TagsSorter { get; set; }

    /// <summary>
    /// Provides a mechanism to be notified when Swagger UI has finished rendering a newly provided definition.
    /// </summary>
    [JsonIgnore]
    public string? OnComplete { get; set; }

    /// <summary>
    /// Syntax highlighting of payloads and cURL command.
    /// </summary>
    public SyntaxHighlightOptions? SyntaxHighlight { get; set; }

    /// <summary>
    /// If set to true, it persists authorization data and it would not be lost on browser close/refresh
    /// </summary>
    public bool PersistAuthorization { get; set; }

    /// <summary>
    /// Controls whether the "Try it out" section should be enabled by default.
    /// </summary>
    public bool? TryItOutEnabled { get; set; }

    /// <summary>
    /// Enables the request snippet section. When disabled, the legacy curl snippet will be used.
    /// </summary>
    public bool? RequestSnippetsEnabled { get; set; }

    /// <summary>
    /// Configuration section for the requestSnippets plugin.
    /// </summary>
    [JsonIgnore]
    public string? RequestSnippets { get; set; }

    /// <summary>
    /// OAuth redirect URL
    /// </summary>
    [JsonPropertyName("oauth2RedirectUrl")]
    public string? OAuth2RedirectUrl { get; set; }

    /// <summary>
    /// List of HTTP methods that have the Try it out feature enabled.
    /// An empty array disables Try it out for all operations. This does not filter the operations from the display
    /// </summary>
    public IEnumerable<SubmitMethod> SupportedSubmitMethods { get; set; } = Enum.GetValues<SubmitMethod>();

    /// <summary>
    /// By default, Swagger-UI attempts to validate specs against swagger.io's online validator.
    /// You can use this parameter to set a different validator URL, for example for locally deployed validators (Validator Badge).
    /// Setting it to null will disable validation
    /// </summary>
    public string? ValidatorUrl { get; set; }

    [JsonExtensionData]
    public Dictionary<string, object> AdditionalItems { get; set; } = [];

    /// <summary>
    /// Gets the JavaScript config object, represented as JSON, that will be passed to the initOAuth method
    /// </summary>
    [JsonIgnore]
    public OAuthOptions OAuthOptions { get; set; } = new OAuthOptions();

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

public sealed class SyntaxHighlightOptions
{
    /// <summary>
    /// Whether syntax highlighting should be activated or not.
    /// </summary>
    public bool? Activated { get; set; }

    /// <summary>
    /// Syntax coloring theme to use.
    /// </summary>
    public SyntaxHighlightThemeType? Theme { get; set; }
}

public enum SyntaxHighlightThemeType
{
    Agate,
    Arta,
    Monokai,
    Nord,
    Obsidian,
    // TODO: Implement
    // https://stackoverflow.com/questions/59059989/system-text-json-how-do-i-specify-a-custom-name-for-an-enum-value
    //[EnumMember(Value = "tomorrow-night")]
    //TomorrowNight,
    Idea,
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
