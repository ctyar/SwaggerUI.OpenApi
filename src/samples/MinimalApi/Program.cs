using System.ComponentModel.DataAnnotations;

namespace MinimalApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi("public");
        builder.Services.AddOpenApi("internal");

        builder.Services.AddSwaggerUI("public");
        builder.Services.AddSwaggerUI("internal");

        var app = builder.Build();

        app.MapOpenApi();
        app.MapSwaggerUi();

        app.MapGet("/products", ([Range(0, 10000)] int id, [MinLength(3)][MaxLength(50)] string name,
            [RegularExpression("\\d\\d-\\d\\d")] string code) => id)
            .WithGroupName("public");

        app.MapPost("/todos", (Todo todo) => Results.Created($"/todos/{todo.Id}", todo))
            .WithGroupName("public");

        app.MapGet("/users", () => new[] { "alice", "bob" })
            .WithGroupName("internal");

        app.MapPost("/users", () => Results.Created("/users/1", new { Id = 1, Name = "Test user" }))
            .WithGroupName("internal"); ;

        app.MapControllers();

        app.Run();
    }

    internal record Todo(int Id, string Title, bool Completed, DateTime CreatedAt);
}
