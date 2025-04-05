namespace SingleDocument;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi();

        builder.Services.AddSwaggerUI();

        var app = builder.Build();

        app.MapOpenApi();

        app.MapSwaggerUI();

        app.MapGet("/todos", () => Results.Ok("To do!"));

        app.MapControllers();

        app.Run();
    }
}
