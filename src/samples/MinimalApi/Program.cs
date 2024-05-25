using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MinimalApi.Transformers;

namespace MinimalApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddAuthentication().AddJwtBearer();

        builder.Services.AddOpenApi("v1", options =>
        {
            options.AddHeader("X-Version", "1.0");
            //options.UseTransformer<BearerSecuritySchemeTransformer>();
        });
        builder.Services.AddOpenApi("v2", options =>
        {
            options.UseTransformer(new AddContactTransformer());
            options.UseTransformer((document, context, token) =>
            {
                document.Info.License = new OpenApiLicense { Name = "MIT" };
                return Task.CompletedTask;
            });
        });
        builder.Services.AddOpenApi("responses");
        builder.Services.AddOpenApi("forms");

        var app = builder.Build();

        app.MapOpenApi();
        app.MapSwaggerUi();

        var forms = app.MapGroup("forms")
            .WithGroupName("forms");

        forms.DisableAntiforgery();

        forms.MapPost("/form-file", (IFormFile resume) => Results.Ok(resume.FileName));
        forms.MapPost("/form-files", (IFormFileCollection files) => Results.Ok(files.Count));
        forms.MapPost("/form-file-multiple", (IFormFile resume, IFormFileCollection files) => Results.Ok(files.Count + resume.FileName));
        forms.MapPost("/form-todo", ([FromForm] Todo todo) => Results.Ok(todo));
        forms.MapPost("/forms-pocos-and-files", ([FromForm] Todo todo, IFormFile file) => Results.Ok(new { Todo = todo, File = file.FileName }));

        var v1 = app.MapGroup("v1")
            .WithGroupName("v1");
        var v2 = app.MapGroup("v2")
            .WithGroupName("v2");
        var responses = app.MapGroup("responses")
            .WithGroupName("responses");

        v1.MapGet("/array-of-guids", (Guid[] guids) => guids);

        v1.MapPost("/todos", (Todo todo) => Results.Created($"/todos/{todo.Id}", todo))
            .WithSummary("Creates a new todo item.");
        v1.MapGet("/todos/{id}", (int id) => new TodoWithDueDate(1, "Test todo", false, DateTime.Now.AddDays(1), DateTime.Now))
            .WithDescription("Returns a specific todo item.");

        v2.MapGet("/users", () => new[] { "alice", "bob" })
            .WithTags("users");

        v2.MapPost("/users", () => Results.Created("/users/1", new { Id = 1, Name = "Test user" }));

        responses.MapGet("/200-add-xml", () => new TodoWithDueDate(1, "Test todo", false, DateTime.Now.AddDays(1), DateTime.Now))
            .Produces<Todo>(additionalContentTypes: "text/xml");

        responses.MapGet("/200-only-xml", () => new TodoWithDueDate(1, "Test todo", false, DateTime.Now.AddDays(1), DateTime.Now))
            .Produces<Todo>(contentType: "text/xml");

        responses.MapGet("/triangle", () => new Triangle { Color = "red", Sides = 3, Hypotenuse = 5.0 });
        responses.MapGet("/shape", () => new Shape { Color = "blue", Sides = 4 });

        app.MapControllers();

        app.Run();
    }

    internal record Todo(int Id, string Title, bool Completed, DateTime CreatedAt);
    internal record TodoWithDueDate(int Id, string Title, bool Completed, DateTime CreatedAt, DateTime DueDate)
        : Todo(Id, Title, Completed, CreatedAt);

    [JsonDerivedType(typeof(Triangle), typeDiscriminator: "triangle")]
    [JsonDerivedType(typeof(Square), typeDiscriminator: "square")]
    internal class Shape
    {
        internal string Color { get; set; } = string.Empty;
        internal int Sides { get; set; }
    }

    internal class Triangle : Shape
    {
        internal double Hypotenuse { get; set; }
    }
    internal class Square : Shape
    {
        internal double Area { get; set; }
    }
}
