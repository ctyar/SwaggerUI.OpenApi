using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace SwaggerUI;

internal sealed class DataTypeSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken _)
    {
        if (context.JsonTypeInfo.Type == typeof(TimeOnly) || context.JsonTypeInfo.Type == typeof(TimeOnly?) ||
            context.JsonTypeInfo.Type == typeof(TimeSpan) || context.JsonTypeInfo.Type == typeof(TimeSpan?))
        {
            schema.Example = new OpenApiString(
                TimeProvider.System.GetLocalNow().ToString("HH:mm:ss", CultureInfo.InvariantCulture));
        }
        else if (context.JsonTypeInfo.Type == typeof(string))
        {
            if (context.JsonPropertyInfo?.AttributeProvider?.IsDefined(typeof(EmailAddressAttribute), false) == true)
            {
                schema.Format = "email";
            }
        }

        return Task.CompletedTask;
    }
}
