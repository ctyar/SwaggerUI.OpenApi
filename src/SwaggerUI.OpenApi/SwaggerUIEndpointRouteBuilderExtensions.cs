using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SwaggerUI;

namespace Microsoft.AspNetCore.Builder;

public static class SwaggerUIEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Register endpoints onto the current application for resolving the SwaggerUI associated
    /// with the current application.
    /// </summary>
    /// <param name="routeBuilder">The <see cref="IEndpointRouteBuilder"/>.</param>
    /// <returns>An <see cref="IEndpointRouteBuilder"/> that can be used to further customize the endpoints.</returns>
    public static RouteGroupBuilder MapSwaggerUI(this IEndpointRouteBuilder routeBuilder)
    {
        var group = routeBuilder.MapGroup("swagger")
            .ExcludeFromDescription();

        return MapSwaggerUIFromGroup(group);
    }

    /// <summary>
    /// Register endpoints onto the current application for resolving the SwaggerUI associated
    /// with the current application.
    /// </summary>
    /// <param name="routeBuilder">The <see cref="IEndpointRouteBuilder"/>.</param>
    /// <returns>An <see cref="IEndpointRouteBuilder"/> that can be used to further customize the endpoints.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static RouteGroupBuilder MapSwaggerUIFromGroup(RouteGroupBuilder group)
    {
        group.MapGet("", (HttpContext context) =>
            Results.Content(Endpoints.GetDefaultIndex(context), "text/html"));

        group.MapGet("{documentName}", (HttpContext context, string documentName) =>
            Results.Content(Endpoints.GetIndex(context, documentName), "text/html"));

        group.MapGet("oauth2-redirect.html", () =>
            Results.Content(Endpoints.GetOAuthRedirectHtml(), "text/html"));

        return group;
    }
}
