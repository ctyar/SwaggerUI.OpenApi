using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace MinimalApi.Transformers;

public sealed class AddContactTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info.Contact = new OpenApiContact
        {
            Name = "OpenAPI Enthusiast",
            Email = "iloveopenapi@example.com"
        };
        return Task.CompletedTask;
    }
}
