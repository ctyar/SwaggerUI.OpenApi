using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SwaggerUI.OpenApi.Tests.SwaggerUIServiceCollectionTests;

public class SingleDocumentWithConfigTests2
{
    [Fact]
    public async Task AddSwaggerUIForOneDocumentWithConfig()
    {

        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();
        builder.WebHost.UseTestServer();

        var app = builder.Build();
        app.MapSwaggerUI();
        app.MapGet("/health", () => "Healthy");
        app.Start();

        var client = app.GetTestClient();

        var response = await client.GetStringAsync("/swagger/oauth2-redirect.html", TestContext.Current.CancellationToken);
    }
}
