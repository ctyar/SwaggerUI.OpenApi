namespace TwoDocuments;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi("doc1");
        builder.Services.AddOpenApi("doc2");

        builder.Services.AddSwaggerUI();

        var app = builder.Build();

        app.MapOpenApi();

        app.MapSwaggerUI();

        app.MapGet("/todos", () => Results.Ok("To do!"))
            .WithGroupName("doc1");

        app.MapGet("/users", () => Results.Ok("User!"))
            .WithGroupName("doc2");

        app.MapControllers();

        app.Run();
    }
}
