namespace SwaggerUI.OpenApi.Tests;

internal class Constants
{
#if NET10_0_OR_GREATER
    public const string OpenApiVersion = "3.1.1";
#else
    public const string OpenApiVersion = "3.0.1";
#endif
}
