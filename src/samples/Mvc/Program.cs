namespace Mvc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddOpenApi("v1", o =>
        {
            o.UseTransformer(new BearerSecuritySchemeTransformer("https://demo.duendesoftware.com/connect/authorize",
                "https://demo.duendesoftware.com/connect/token", ["openid", "profile", "email", "api", "offline_access"]));
        });

        var app = builder.Build();

        app.MapOpenApi();
        app.MapSwaggerUi(new SwaggerUiOptions
        {
            OAuthOptions = new OAuthOptions
            {
                ClientId = "interactive.public",
                UsePkceWithAuthorizationCodeGrant = true,
                Scopes = ["openid", "profile", "email", "api", "offline_access"]
            }
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
