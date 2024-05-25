using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace SwaggerUi;

internal sealed class SecurityRequirementsTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        foreach (var item in context.DescriptionGroups.SelectMany(i => i.Items))
        {
            var actionMetadata = item.ActionDescriptor.EndpointMetadata;

            var isAuthorized = actionMetadata.Any(item => item is AuthorizeAttribute);
            var allowAnonymous = actionMetadata.Any(item => item is AllowAnonymousAttribute);

            if (!isAuthorized || allowAnonymous)
            {
                continue;
            }

            if (!item.SupportedResponseTypes.Any(i => i.StatusCode == 401))
            {
                item.SupportedResponseTypes.Add(new ApiResponseType
                {
                    StatusCode = 401,
                });
            }

            if (!item.SupportedResponseTypes.Any(i => i.StatusCode == 403))
            {
                item.SupportedResponseTypes.Add(new ApiResponseType
                {
                    StatusCode = 403,
                });
            }
        }

        return Task.CompletedTask;
    }
}
