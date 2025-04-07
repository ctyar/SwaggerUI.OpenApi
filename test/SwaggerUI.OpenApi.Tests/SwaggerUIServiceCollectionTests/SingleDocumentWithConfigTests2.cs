using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SwaggerUI.OpenApi.Tests.SwaggerUIServiceCollectionTests;

public class SingleDocumentWithConfigTests2
{
    [Fact]
    public async Task AddSwaggerUIForOneDocumentWithConfig()
    {
        /*var builder = WebApplication.CreateBuilder();

        builder.Services.AddControllers();

        var app = builder.Build();

        app.MapGet("/todos", () => "Hello!");

        app.MapControllers();

        var t = Task.Run(() => app.Run(), TestContext.Current.CancellationToken);
        while (true)
        {
            try
            {
                var ser = new TestServer(app.Services);
                var c = ser.CreateClient();

                var s = await c.GetStringAsync("/todos", TestContext.Current.CancellationToken);
            }
            catch (Exception)
            {
                await Task.Delay(1000, TestContext.Current.CancellationToken);
            }
        }*/

        /*var server = new TestServer(new WebHostBuilder()
            .UseTestServer()
            .ConfigureServices(services =>
            {
                services.AddControllers();
            })
            .Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/books", () => "Hi!");
                    endpoints.MapSwaggerUI();
                });
            }));

        var client = server.CreateClient();

        var s = await client.GetStringAsync("/swagger/oauth2-redirect.html", TestContext.Current.CancellationToken);*/

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

    public class Test : WebApplicationFactory<SingleDocumentConfig.Program>
    {
        protected override IWebHostBuilder? CreateWebHostBuilder()
        {
            var builder = WebApplication.CreateBuilder();

            return builder.WebHost;
        }
    }
}
