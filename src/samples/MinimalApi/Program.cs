namespace MinimalApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi("public");
        builder.Services.AddOpenApi("internal");

        builder.Services.AddSwaggerUi("public");
        builder.Services.AddSwaggerUi("internal");

        var app = builder.Build();

        app.MapOpenApi();
        app.MapSwaggerUi();

        app.MapGet("/array-of-guids", (Guid[] guids) => guids)
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
