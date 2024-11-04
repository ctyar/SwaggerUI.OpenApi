using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace SwaggerUI;

internal sealed class SecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    private const string SchemeId = "Bearer";

    private readonly Uri _authorizationUrl;
    private readonly Uri _tokenUrl;
    private readonly string[] _scopes;

    public SecuritySchemeTransformer(Uri authorizationUrl, Uri tokenUrl, string[] scopes)
    {
        _authorizationUrl = authorizationUrl;
        _tokenUrl = tokenUrl;
        _scopes = scopes;
    }

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var schemes = new Dictionary<string, OpenApiSecurityScheme>
        {
            [SchemeId] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = _authorizationUrl,
                        TokenUrl = _tokenUrl,
                        Scopes = _scopes?.ToDictionary(item => item, item => item),
                    }
                }
            }
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = schemes;

        foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
        {
            if (operation.Value.Responses.Any(r => r.Key == "401"))
            {
                operation.Value.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = SchemeId,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            }
        }

        return Task.CompletedTask;
    }
}
