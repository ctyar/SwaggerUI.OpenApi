using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;

namespace MinimalApi.Transformers;

public static class OperationTransformers
{
    public static OpenApiOptions AddHeader(this OpenApiOptions options, string headerName, string defaultValue)
    {
        return options.UseOperationTransformer((operation, context, cancellationToken) =>
        {
            var schema = OpenApiTypeMapper.MapTypeToOpenApiPrimitiveType(typeof(string));
            schema.Default = new OpenApiString(defaultValue);
            operation.Parameters ??= [];
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = headerName,
                In = ParameterLocation.Header,
                Schema = schema
            });
            return Task.CompletedTask;
        });
    }
}
