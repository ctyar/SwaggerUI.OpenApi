using System.Text.Json;
using System.Text.Json.Serialization;
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
        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddOpenApi(o =>
        {
            o.AddIdentityServer("https://demo.duendesoftware.com", ["openid", "profile", "email", "api", "offline_access"]);
        });
        builder.Services.AddSwaggerUI(o =>
        {
            o.AddIdentityServer("interactive.public", ["openid", "profile", "email", "api", "offline_access"]);
        });

        var app = builder.Build();

        app.MapOpenApi();
        app.MapSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
