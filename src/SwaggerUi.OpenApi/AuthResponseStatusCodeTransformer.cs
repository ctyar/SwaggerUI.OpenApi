using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace SwaggerUi;

internal static class AuthResponseStatusCodeTransformer
{
    public static Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context,
        CancellationToken _)
    {
        var actionMetadata = context.Description.ActionDescriptor.EndpointMetadata;

        var isAuthorized = actionMetadata.Any(item => item is AuthorizeAttribute);
        var allowAnonymous = actionMetadata.Any(item => item is AllowAnonymousAttribute);

        if (!isAuthorized || allowAnonymous)
        {
            return Task.CompletedTask;
        }

        if (!operation.Responses.Any(r => r.Key == "401"))
        {
            operation.Responses.Add("401", new OpenApiResponse
            {
                Description = "Unauthorized"
            });
        }

        if (!operation.Responses.Any(r => r.Key == "403"))
        {
            operation.Responses.Add("403", new OpenApiResponse
            {
                Description = "Forbidden"
            });
        }

        return Task.CompletedTask;
    }
}
