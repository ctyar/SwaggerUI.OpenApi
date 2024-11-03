using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SwaggerUi;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// SwaggerUi-related methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class SwaggerUiServiceCollectionExtensions
{
    /// <summary>
    /// Adds SwaggerUi services related to the given document name to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services onto.</param>
    /// <param name="documentName">The name of the OpenAPI document associated with registered services.</param>
    public static IServiceCollection AddSwaggerUi(this IServiceCollection services, string documentName)
    {
        return services.AddSwaggerUi(documentName, _ => { });
    }

    /// <summary>
    /// Adds SwaggerUi services related to the given document name to the specified <see cref="IServiceCollection"/>
    /// with the specified options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services onto.</param>
    /// <param name="documentName">The name of the OpenAPI document associated with registered services.</param>
    /// <param name="configureOptions">A delegate used to configure the target <see cref="SwaggerUiOptions"/>.</param>
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

    /// <summary>
    /// Adds SwaggerUi services related to the default document to the specified <see cref="IServiceCollection"/>
    /// with the specified options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services onto.</param>
    /// <param name="configureOptions">A delegate used to configure the target <see cref="SwaggerUiOptions"/>.</param>
    public static IServiceCollection AddSwaggerUi(this IServiceCollection services, Action<SwaggerUiOptions> configureOptions)
    {
        return services.AddSwaggerUi(SwaggerUiConstants.DefaultDocumentName, configureOptions);
    }
}
