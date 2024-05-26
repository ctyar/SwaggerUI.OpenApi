using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.OpenApi.Models;

namespace SwaggerUi;

internal sealed class AuthResponseStatusCodeTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        foreach (var item in context.DescriptionGroups.SelectMany(i => i.Items))
        {
            var actionMetadata = item.ActionDescriptor.EndpointMetadata;

            var isAuthorized = actionMetadata.Any(item => item is AuthorizeAttribute);
            var allowAnonymous = actionMetadata.Any(item => item is AllowAnonymousAttribute);

            if (!isAuthorized || allowAnonymous)
            {
                continue;
            }

            var path = MapRelativePathToItemPath(item);
            var operationType = GetOperationType(item);

            var responses = document.Paths[path].Operations[operationType].Responses;

            if (!responses.Any(r => r.Key == "401"))
            {
                responses.Add("401", new OpenApiResponse
                {
                    Description = "Unauthorized"
                });
            }

            if (!responses.Any(r => r.Key == "403"))
            {
                responses.Add("403", new OpenApiResponse
                {
                    Description = "Forbidden"
                });
            }
        }

        return Task.CompletedTask;
    }

    // Following methods are borrowed from
    // https://github.com/dotnet/aspnetcore/blob/main/src/OpenApi/src/Extensions/ApiDescriptionExtensions.cs

    /// <summary>
    /// Maps the relative path included in the ApiDescription to the path
    /// that should be included in the OpenApiDocument. This typically
    /// consists of removing any constraints from route parameter parts
    /// and retaining only the literals.
    /// </summary>
    /// <param name="apiDescription">The ApiDescription to resolve an item path from.</param>
    /// <returns>The resolved item path for the given <paramref name="apiDescription"/>.</returns>
    private static string MapRelativePathToItemPath(ApiDescription apiDescription)
    {
        Debug.Assert(apiDescription.RelativePath != null, "Relative path cannot be null.");
        // "" -> "/"
        if (string.IsNullOrEmpty(apiDescription.RelativePath))
        {
            return "/";
        }
        var strippedRoute = new StringBuilder();
        var routePattern = RoutePatternFactory.Parse(apiDescription.RelativePath);
        for (var i = 0; i < routePattern.PathSegments.Count; i++)
        {
            strippedRoute.Append('/');
            var segment = routePattern.PathSegments[i];
            foreach (var part in segment.Parts)
            {
                if (part is RoutePatternLiteralPart literalPart)
                {
                    strippedRoute.Append(literalPart.Content);
                }
                else if (part is RoutePatternParameterPart parameterPart)
                {
                    strippedRoute.Append('{');
                    strippedRoute.Append(parameterPart.Name);
                    strippedRoute.Append('}');
                }
                else if (part is RoutePatternSeparatorPart separatorPart)
                {
                    strippedRoute.Append(separatorPart.Content);
                }
            }
        }
        return strippedRoute.ToString();
    }

    /// <summary>
    /// Maps the HTTP method of the ApiDescription to the OpenAPI <see cref="OperationType"/> .
    /// </summary>
    /// <param name="apiDescription">The ApiDescription to resolve an operation type from.</param>
    /// <returns>The <see cref="OperationType"/> associated with the given <paramref name="apiDescription"/>.</returns>
    private static OperationType GetOperationType(ApiDescription apiDescription) =>
        apiDescription.HttpMethod?.ToUpperInvariant() switch
        {
            "GET" => OperationType.Get,
            "POST" => OperationType.Post,
            "PUT" => OperationType.Put,
            "DELETE" => OperationType.Delete,
            "PATCH" => OperationType.Patch,
            "HEAD" => OperationType.Head,
            "OPTIONS" => OperationType.Options,
            "TRACE" => OperationType.Trace,
            _ => throw new InvalidOperationException($"Unsupported HTTP method: {apiDescription.HttpMethod}"),
        };
}
