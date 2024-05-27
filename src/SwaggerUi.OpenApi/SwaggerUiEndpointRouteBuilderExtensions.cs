using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SwaggerUi;

namespace Microsoft.AspNetCore.Builder;

public static class SwaggerUiEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Helper method to render Swagger UI view.
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IEndpointConventionBuilder MapSwaggerUi(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/swagger", (HttpContext context) =>
            Results.Content(Endpoints.GetIndex2(context), "text/html")
        ).ExcludeFromDescription();

        routeBuilder.MapGet("/swagger/{documentName}",
            (HttpContext context, string documentName) => Results.Content(Endpoints.GetIndex(context, documentName), "text/html")
        ).ExcludeFromDescription();

        return routeBuilder.MapGet("/swagger/oauth2-redirect.html",
            () => Results.Content(Endpoints.GetOAuthRedirectHtml(), "text/html")
        )
        .ExcludeFromDescription();
    }
}
