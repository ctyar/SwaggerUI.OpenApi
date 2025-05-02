using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace SwaggerUI;

internal sealed class DataTypeSchemaTransformer : IOpenApiSchemaTransformer
{
    internal static TimeProvider TimeProvider { get; set; } = TimeProvider.System;

    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken _)
    {
        if (context.JsonTypeInfo.Type == typeof(TimeOnly) || context.JsonTypeInfo.Type == typeof(TimeOnly?) ||
            context.JsonTypeInfo.Type == typeof(TimeSpan) || context.JsonTypeInfo.Type == typeof(TimeSpan?))
        {
            schema.Example = new OpenApiString(
                TimeProvider.GetLocalNow().ToString("HH:mm:ss", CultureInfo.InvariantCulture));
        }

        var emailProperties = context.JsonTypeInfo.Type.GetProperties()
            .Where(p => p.GetCustomAttribute<EmailAddressAttribute>(true) is not null)
            .Select(prop => prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ??
                context.JsonTypeInfo.Options.PropertyNamingPolicy?.ConvertName(prop.Name) ??
                prop.Name)
            .ToList();

        foreach (var emailProperty in emailProperties)
        {
            schema.Properties[emailProperty].Format = "email";
        }

        return Task.CompletedTask;
    }
}
