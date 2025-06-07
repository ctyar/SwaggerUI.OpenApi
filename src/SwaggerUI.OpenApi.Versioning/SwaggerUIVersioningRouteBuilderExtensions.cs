using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder;

public static class SwaggerUIVersioningRouteBuilderExtensions
{
    /// <summary>
    /// Register endpoints onto the current application for resolving the SwaggerUI associated
    /// with the current application.
    /// </summary>
    /// <param name="routeBuilder">The <see cref="IEndpointRouteBuilder"/>.</param>
    /// <returns>An <see cref="IEndpointRouteBuilder"/> that can be used to further customize the endpoints.</returns>
    public static IEndpointConventionBuilder MapSwaggerUIVersioning(this WebApplication app)
    {
        var versionedEndpointRouteBuilder = app.NewVersionedApi();

        var group = versionedEndpointRouteBuilder.MapGroup("swagger")
            .ExcludeFromDescription()
            .IsApiVersionNeutral();

        return SwaggerUIEndpointRouteBuilderExtensions.MapSwaggerUIFromGroup(group);
    }
}