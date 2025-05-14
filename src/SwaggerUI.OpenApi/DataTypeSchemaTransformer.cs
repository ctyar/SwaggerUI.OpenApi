using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
#if NET10_0_OR_GREATER
            schema.Example = JsonValue.Create(TimeProvider.GetLocalNow().ToString("HH:mm:ss", CultureInfo.InvariantCulture));
#else
            schema.Example = new OpenApiString(TimeProvider.GetLocalNow().ToString("HH:mm:ss", CultureInfo.InvariantCulture));
#endif
        }

        // This only works for parameters without a class like:
        // app.MapPost("todos", ([EmailAddress] string email) => Results.Ok);
        if (context.ParameterDescription?.ParameterDescriptor is IParameterInfoParameterDescriptor descriptor)
        {
            var emailAddressAttribute = descriptor.ParameterInfo
                .GetCustomAttribute<EmailAddressAttribute>(inherit: true);

            if (emailAddressAttribute is not null)
            {
                schema.Format = "email";
            }
        }

        // This only works for properties inside a class like:
        // app.MapPost("todos", (Request req) => Results.Ok);
        // class Request { [EmailAddress] public string Email { get; set; } }
        var emailProperties = context.JsonTypeInfo.Type.GetProperties()
            .Where(p => p.GetCustomAttribute<EmailAddressAttribute>(true) is not null)
            .Select(prop => prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ??
                context.JsonTypeInfo.Options.PropertyNamingPolicy?.ConvertName(prop.Name) ??
                prop.Name)
            .ToList();

        foreach (var emailProperty in emailProperties)
        {
#if NET10_0_OR_GREATER
            if (schema.Properties?.TryGetValue(emailProperty, out var propertySchema) == true)
            {
                var openApiSchema = (OpenApiSchema)propertySchema;
                openApiSchema.Format = "email";
            }
#else
            schema.Properties[emailProperty].Format = "email";
#endif
        }

        return Task.CompletedTask;
    }
}
