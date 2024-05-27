using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;

namespace Mvc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.Authority = "https://demo.duendesoftware.com/";
                o.Audience = "api";
            });
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();


        builder.Services.AddOpenApi("v1", o =>
        {
            o.AddIdentityServer("https://demo.duendesoftware.com", ["openid", "profile", "email", "api", "offline_access"]);
        });
        builder.Services.AddSwaggerUi("v1", o =>
        {
            o.AddIdentityServer("interactive.public", ["openid", "profile", "email", "api", "offline_access"]);
        });

        builder.Services.AddOpenApi("v2", o =>
        {
            o.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
        });
        builder.Services.AddSwaggerUi("v2");


        var app = builder.Build();

        app.MapOpenApi();
        app.MapSwaggerUi();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
