using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
#if NET10_0_OR_GREATER
using Microsoft.OpenApi.Models.Interfaces;
using Microsoft.OpenApi.Models.References;
#endif

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
#if NET10_0_OR_GREATER
        var schemes = new Dictionary<string, IOpenApiSecurityScheme>();
#else
        var schemes = new Dictionary<string, OpenApiSecurityScheme>();
#endif

        schemes[SchemeId] = new OpenApiSecurityScheme
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
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = schemes;

        foreach (var value in document.Paths.Values)
        {
            if (value.Operations is null)
            {
                continue;
            }

            foreach (var operation in value.Operations)
            {
                if (operation.Value?.Responses is null)
                {
                    continue;
                }

                if (operation.Value.Responses.Any(r => r.Key == "401"))
                {
                    operation.Value.Security ??= [];


                    operation.Value.Security.Add(new OpenApiSecurityRequirement
                    {
                        {
#if NET10_0_OR_GREATER
                            new OpenApiSecuritySchemeReference(SchemeId, document),
#else
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = SchemeId,
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
#endif
                            []
                        }
                    });
                }
            }
        }

        return Task.CompletedTask;
    }
}