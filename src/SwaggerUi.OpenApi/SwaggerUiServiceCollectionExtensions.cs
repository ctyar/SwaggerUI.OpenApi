using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

public static class SwaggerUiServiceCollectionExtensions
{
    /// <summary>
    /// Helper method to render Swagger UI view.
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerUi(this IServiceCollection services, string documentName)
    {
        return services.AddSwaggerUi(documentName, _ => { });
    }

    public static IServiceCollection AddSwaggerUi(this IServiceCollection services, string documentName,
        Action<SwaggerUiOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        services.Configure<SwaggerUiOptions>(documentName, options =>
        {
            configureOptions(options);

            if (!options.Urls.Any())
            {
                options.Urls = [new UrlDescriptor { Name = documentName, Url = $"/openapi/{documentName}.json" }];
            }
        });

        return services;
    }

    public static IServiceCollection AddSwaggerUi(this IServiceCollection services, Action<SwaggerUiOptions> configureOptions)
    {
        return services.AddSwaggerUi("v1", configureOptions);
    }
}
