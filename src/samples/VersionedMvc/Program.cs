using Asp.Versioning;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddApiVersioning(options =>
        {
            options.ApiVersionReader = new UrlSegmentApiVersionReader();

        })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddOpenApi();

        builder.Services.AddSwaggerUI();

        var app = builder.Build();

        app.MapOpenApi().WithDocumentPerVersion();
        app.MapSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}