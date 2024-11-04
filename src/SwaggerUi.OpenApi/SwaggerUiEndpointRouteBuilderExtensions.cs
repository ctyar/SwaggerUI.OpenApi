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
    public static IEndpointConventionBuilder MapSwaggerUI(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/swagger", (HttpContext context) =>
            Results.Content(Endpoints.GetDefaultIndex(context), "text/html")
        ).ExcludeFromDescription();

        routeBuilder.MapGet("/swagger/{documentName}", (HttpContext context, string documentName) =>
            Results.Content(Endpoints.GetIndex(context, documentName), "text/html")
        ).ExcludeFromDescription();

        return routeBuilder.MapGet("/swagger/oauth2-redirect.html",
            () => Results.Content(Endpoints.GetOAuthRedirectHtml(), "text/html")
        )
        .ExcludeFromDescription();
    }
}
