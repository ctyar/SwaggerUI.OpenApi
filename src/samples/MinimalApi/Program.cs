using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MinimalApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddOpenApi("public");
        builder.Services.AddOpenApi("internal");

        builder.Services.AddSwaggerUI("public");
        builder.Services.AddSwaggerUI("internal");

        var app = builder.Build();

        app.MapOpenApi();
        app.MapSwaggerUI();

        app.MapGet("/products", ([Range(0, 10000)] int id, [MinLength(3)][MaxLength(50)] string name,
            [RegularExpression("\\d\\d-\\d\\d")] string code) => id)
            .WithGroupName("public");

        app.MapPost("/todos", (Todo todo) => Results.Created($"/todos/{todo.Id}", todo))
            .WithGroupName("public");

        app.MapPost("/request", (SampleRequest req) => Results.Created())
            .WithGroupName("public"); ;

        app.MapGet("/users", () => new[] { "alice", "bob" })
            .WithGroupName("internal");

        app.MapPost("/users", () => Results.Created("/users/1", new { Id = 1, Name = "Test user" }))
            .WithGroupName("internal"); ;

        app.Run();
    }

    internal record Todo(int Id, string Title, bool Completed, DateTime CreatedAt);
}

public class SampleRequest
{
    public string String { get; set; } = null!;
    public bool Bool { get; set; }
    public byte Byte { get; set; }
    public sbyte Sbyte { get; set; }
    public char Char { get; set; }
    public decimal Decimal { get; set; }
    public double Double { get; set; }
    public float Float { get; set; }
    public int Int { get; set; }
    public uint Uint { get; set; }
    public long Long { get; set; }
    public ulong Ulong { get; set; }
    public short Short { get; set; }
    public ushort UShort { get; set; }
    public DateTime DateTime { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public DateOnly DateOnly { get; set; }
    public TimeOnly TimeOnly { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public object Object { get; set; } = null!;
    public dynamic Dynamic { get; set; } = null!;
    public int[] Array { get; set; } = null!;
    public List<int> List { get; set; } = null!;
    public IEnumerable<int> IEnumerable { get; set; } = null!;
    public Dictionary<int, string> Dictionary { get; set; } = null!;
    public Guid Guid { get; set; }
    public Uri Uri { get; set; } = null!;
    //[EmailAddress]
    //public string Email { get; set; } = null!; // TODO fix
    //public string IPAddress { get; set; } = null!; // TODO Fix
}
