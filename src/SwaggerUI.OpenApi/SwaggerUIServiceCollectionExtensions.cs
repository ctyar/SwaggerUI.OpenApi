using System;
using Microsoft.Extensions.DependencyInjection;
using SwaggerUI;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// SwaggerUI-related methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class SwaggerUIServiceCollectionExtensions
{
    /// <summary>
    /// Adds SwaggerUI services related to the default document to the specified <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services onto.</param>
    public static IServiceCollection AddSwaggerUI(this IServiceCollection services)
    {
        return services.AddSwaggerUI(SwaggerUIConstants.DefaultDocumentName);
    }

    /// <summary>
    /// Adds SwaggerUI services related to the given document name to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services onto.</param>
    /// <param name="documentName">The name of the OpenAPI document associated with registered services.</param>
    public static IServiceCollection AddSwaggerUI(this IServiceCollection services, string documentName)
    {
        return services.AddSwaggerUI(documentName, _ => { });
    }

    /// <summary>
    /// Adds SwaggerUI services related to the default document to the specified <see cref="IServiceCollection"/>
    /// with the specified options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services onto.</param>
    /// <param name="configureOptions">A delegate used to configure the target <see cref="SwaggerUIOptions"/>.</param>
    public static IServiceCollection AddSwaggerUI(this IServiceCollection services, Action<SwaggerUIOptions> configureOptions)
    {
        return services.AddSwaggerUI(SwaggerUIConstants.DefaultDocumentName, configureOptions);
    }

    /// <summary>
    /// Adds SwaggerUI services related to the given document name to the specified <see cref="IServiceCollection"/>
    /// with the specified options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services onto.</param>
    /// <param name="documentName">The name of the OpenAPI document associated with registered services.</param>
    /// <param name="configureOptions">A delegate used to configure the target <see cref="SwaggerUIOptions"/>.</param>
    public static IServiceCollection AddSwaggerUI(this IServiceCollection services, string documentName,
        Action<SwaggerUIOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        services.Configure<SwaggerUIOptions>(documentName, options =>
        {
            configureOptions(options);
        });

        return services;
    }
}
