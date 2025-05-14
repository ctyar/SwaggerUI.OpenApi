using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Time.Testing;

namespace SwaggerUI.OpenApi.Tests;

public class DataTypeSchemaTransformerTests
{
    [Fact]
    public async Task TimeOnlyTransformerTest()
    {
        var expected = $$$"""
            {
              "openapi": "{{{Constants.OpenApiVersion}}}",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": {
                "/todos": {
                  "post": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "parameters": [
                      {
                        "name": "timeOnly",
                        "in": "query",
                        "required": true,
                        "schema": {
                          "type": "string",
                          "format": "time",
                          "example": "18:41:23"
                        }
                      }
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "application/json": {
                            "schema": {
                              "$ref": "#/components/schemas/AnonymousTypeOfObjectAndIResult"
                            }
                          }
                        }
                      }
                    }
                  }
                }
              },
              "components": {
                "schemas": {
                  "AnonymousTypeOfObjectAndIResult": { }
                }
              },
              "tags": [
                {
                  "name": "SwaggerUI.OpenApi.Tests"
                }
              ]
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var now = new DateTimeOffset(2025, 04, 23, 18, 41, 23, TimeSpan.Zero);
        var fakeTimeProvider = new FakeTimeProvider(now);
        DataTypeSchemaTransformer.TimeProvider = fakeTimeProvider;

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.MapPost("todos", (TimeOnly timeOnly) => Results.Ok);

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task NullableTimeOnlyTransformerTest()
    {
        var expected = $$$"""
            {
              "openapi": "{{{Constants.OpenApiVersion}}}",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": {
                "/todos": {
                  "post": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "parameters": [
                      {
                        "name": "timeOnly",
                        "in": "query",
                        "schema": {
                          "type": "string",
                          "format": "time",
                          "example": "18:41:23"
                        }
                      }
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "application/json": {
                            "schema": {
                              "$ref": "#/components/schemas/AnonymousTypeOfObjectAndIResult"
                            }
                          }
                        }
                      }
                    }
                  }
                }
              },
              "components": {
                "schemas": {
                  "AnonymousTypeOfObjectAndIResult": { }
                }
              },
              "tags": [
                {
                  "name": "SwaggerUI.OpenApi.Tests"
                }
              ]
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var now = new DateTimeOffset(2025, 04, 23, 18, 41, 23, TimeSpan.Zero);
        var fakeTimeProvider = new FakeTimeProvider(now);
        DataTypeSchemaTransformer.TimeProvider = fakeTimeProvider;

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.MapPost("todos", (TimeOnly? timeOnly) => Results.Ok);

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task TimeSpanTransformerTest()
    {
        var expected = $$$"""
            {
              "openapi": "{{{Constants.OpenApiVersion}}}",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": {
                "/todos": {
                  "post": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "parameters": [
                      {
                        "name": "timeSpan",
                        "in": "query",
                        "required": true,
                        "schema": {
                          "pattern": "^-?(\\d+\\.)?\\d{2}:\\d{2}:\\d{2}(\\.\\d{1,7})?$",
                          "type": "string",
                          "example": "18:41:23"
                        }
                      }
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "application/json": {
                            "schema": {
                              "$ref": "#/components/schemas/AnonymousTypeOfObjectAndIResult"
                            }
                          }
                        }
                      }
                    }
                  }
                }
              },
              "components": {
                "schemas": {
                  "AnonymousTypeOfObjectAndIResult": { }
                }
              },
              "tags": [
                {
                  "name": "SwaggerUI.OpenApi.Tests"
                }
              ]
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var now = new DateTimeOffset(2025, 04, 23, 18, 41, 23, TimeSpan.Zero);
        var fakeTimeProvider = new FakeTimeProvider(now);
        DataTypeSchemaTransformer.TimeProvider = fakeTimeProvider;

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.MapPost("todos", (TimeSpan timeSpan) => Results.Ok);

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task NullableTimeSpanTransformerTest()
    {
        var expected = $$$"""
            {
              "openapi": "{{{Constants.OpenApiVersion}}}",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": {
                "/todos": {
                  "post": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "parameters": [
                      {
                        "name": "timeSpan",
                        "in": "query",
                        "schema": {
                          "pattern": "^-?(\\d+\\.)?\\d{2}:\\d{2}:\\d{2}(\\.\\d{1,7})?$",
                          "type": "string",
                          "example": "18:41:23"
                        }
                      }
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "application/json": {
                            "schema": {
                              "$ref": "#/components/schemas/AnonymousTypeOfObjectAndIResult"
                            }
                          }
                        }
                      }
                    }
                  }
                }
              },
              "components": {
                "schemas": {
                  "AnonymousTypeOfObjectAndIResult": { }
                }
              },
              "tags": [
                {
                  "name": "SwaggerUI.OpenApi.Tests"
                }
              ]
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var now = new DateTimeOffset(2025, 04, 23, 18, 41, 23, TimeSpan.Zero);
        var fakeTimeProvider = new FakeTimeProvider(now);
        DataTypeSchemaTransformer.TimeProvider = fakeTimeProvider;

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.MapPost("todos", (TimeSpan? timeSpan) => Results.Ok);

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task EmailInClassTransformerTest()
    {
        var expected = $$$"""
            {
              "openapi": "{{{Constants.OpenApiVersion}}}",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": {
                "/todos": {
                  "post": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "requestBody": {
                      "content": {
                        "application/json": {
                          "schema": {
                            "$ref": "#/components/schemas/Request"
                          }
                        }
                      },
                      "required": true
                    },
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "application/json": {
                            "schema": {
                              "$ref": "#/components/schemas/AnonymousTypeOfObjectAndIResult"
                            }
                          }
                        }
                      }
                    }
                  }
                }
              },
              "components": {
                "schemas": {
                  "AnonymousTypeOfObjectAndIResult": { },
                  "Request": {
                    "type": "object",
                    "properties": {
                      "email": {
                        "type": "string",
                        "format": "email"
                      }
                    }
                  }
                }
              },
              "tags": [
                {
                  "name": "SwaggerUI.OpenApi.Tests"
                }
              ]
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var now = new DateTimeOffset(2025, 04, 23, 18, 41, 23, TimeSpan.Zero);
        var fakeTimeProvider = new FakeTimeProvider(now);
        DataTypeSchemaTransformer.TimeProvider = fakeTimeProvider;

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.MapPost("todos", (Request req) => Results.Ok);

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task EmailTransformerTest()
    {
        var expected = $$$"""
            {
              "openapi": "{{{Constants.OpenApiVersion}}}",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": {
                "/todos": {
                  "post": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "parameters": [
                      {
                        "name": "email",
                        "in": "query",
                        "required": true,
                        "schema": {
                          "type": "string",
                          "format": "email"
                        }
                      }
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "application/json": {
                            "schema": {
                              "$ref": "#/components/schemas/AnonymousTypeOfObjectAndIResult"
                            }
                          }
                        }
                      }
                    }
                  }
                }
              },
              "components": {
                "schemas": {
                  "AnonymousTypeOfObjectAndIResult": { }
                }
              },
              "tags": [
                {
                  "name": "SwaggerUI.OpenApi.Tests"
                }
              ]
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var now = new DateTimeOffset(2025, 04, 23, 18, 41, 23, TimeSpan.Zero);
        var fakeTimeProvider = new FakeTimeProvider(now);
        DataTypeSchemaTransformer.TimeProvider = fakeTimeProvider;

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.MapPost("todos", ([EmailAddress] string email) => Results.Ok);

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    private class Request
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
