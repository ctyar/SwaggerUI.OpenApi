using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SwaggerUI.OpenApi.Tests;

public class OpenApiOptionsExtensionsTests
{
    [Fact]
    public async Task AddOAuth2()
    {
        var expected = """
            {
              "openapi": "3.0.1",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": { },
              "components": {
                "securitySchemes": {
                  "Bearer": {
                    "type": "oauth2",
                    "flows": {
                      "authorizationCode": {
                        "authorizationUrl": "https://demo.duendesoftware.com/connect/authorize",
                        "tokenUrl": "https://demo.duendesoftware.com/connect/token",
                        "scopes": {
                          "email": "email",
                          "offline_access": "offline_access"
                        }
                      }
                    }
                  }
                }
              }
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi(o =>
        {
            o.AddOAuth2(new Uri("https://demo.duendesoftware.com/connect/authorize"),
                new Uri("https://demo.duendesoftware.com/connect/token"),
                ["email", "offline_access"]);
        });
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddOAuth2WithProtectedEndpoint()
    {
        var expected = """
            {
              "openapi": "3.0.1",
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
                "/admin": {
                  "get": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "text/plain": {
                            "schema": {
                              "type": "string"
                            }
                          }
                        }
                      },
                      "401": {
                        "description": "Unauthorized"
                      },
                      "403": {
                        "description": "Forbidden"
                      }
                    },
                    "security": [
                      {
                        "Bearer": [ ]
                      }
                    ]
                  }
                }
              },
              "components": {
                "securitySchemes": {
                  "Bearer": {
                    "type": "oauth2",
                    "flows": {
                      "authorizationCode": {
                        "authorizationUrl": "https://demo.duendesoftware.com/connect/authorize",
                        "tokenUrl": "https://demo.duendesoftware.com/connect/token",
                        "scopes": {
                          "email": "email",
                          "offline_access": "offline_access"
                        }
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

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi(o =>
        {
            o.AddOAuth2(new Uri("https://demo.duendesoftware.com/connect/authorize"),
                new Uri("https://demo.duendesoftware.com/connect/token"),
                ["email", "offline_access"]);
        });
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();
        app.MapGet("admin", () => "Hi Admin!")
            .RequireAuthorization();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddOAuth2WithMixedEnpoints()
    {
        var expected = """
            {
              "openapi": "3.0.1",
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
                "/profile": {
                  "get": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "text/plain": {
                            "schema": {
                              "type": "string"
                            }
                          }
                        }
                      }
                    }
                  }
                },
                "/admin": {
                  "get": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "text/plain": {
                            "schema": {
                              "type": "string"
                            }
                          }
                        }
                      },
                      "401": {
                        "description": "Unauthorized"
                      },
                      "403": {
                        "description": "Forbidden"
                      }
                    },
                    "security": [
                      {
                        "Bearer": [ ]
                      }
                    ]
                  }
                }
              },
              "components": {
                "securitySchemes": {
                  "Bearer": {
                    "type": "oauth2",
                    "flows": {
                      "authorizationCode": {
                        "authorizationUrl": "https://demo.duendesoftware.com/connect/authorize",
                        "tokenUrl": "https://demo.duendesoftware.com/connect/token",
                        "scopes": {
                          "email": "email",
                          "offline_access": "offline_access"
                        }
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

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi(o =>
        {
            o.AddOAuth2(new Uri("https://demo.duendesoftware.com/connect/authorize"),
                new Uri("https://demo.duendesoftware.com/connect/token"),
                ["email", "offline_access"]);
        });
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();
        app.MapGet("profile", () => "Hi!");
        app.MapGet("admin", () => "Hi Admin!")
            .RequireAuthorization();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddOAuth2WithProtectedEndpointAndAllowAnonymous()
    {
        var expected = """
            {
              "openapi": "3.0.1",
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
                "/admin": {
                  "get": {
                    "tags": [
                      "SwaggerUI.OpenApi.Tests"
                    ],
                    "responses": {
                      "200": {
                        "description": "OK",
                        "content": {
                          "text/plain": {
                            "schema": {
                              "type": "string"
                            }
                          }
                        }
                      }
                    }
                  }
                }
              },
              "components": {
                "securitySchemes": {
                  "Bearer": {
                    "type": "oauth2",
                    "flows": {
                      "authorizationCode": {
                        "authorizationUrl": "https://demo.duendesoftware.com/connect/authorize",
                        "tokenUrl": "https://demo.duendesoftware.com/connect/token",
                        "scopes": {
                          "email": "email",
                          "offline_access": "offline_access"
                        }
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

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi(o =>
        {
            o.AddOAuth2(new Uri("https://demo.duendesoftware.com/connect/authorize"),
                new Uri("https://demo.duendesoftware.com/connect/token"),
                ["email", "offline_access"]);
        });
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();
        app.MapGet("admin", () => "Hi Admin!")
            .RequireAuthorization()
            .AllowAnonymous();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddIdentityServer()
    {
        var expected = """
            {
              "openapi": "3.0.1",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": { },
              "components": {
                "securitySchemes": {
                  "Bearer": {
                    "type": "oauth2",
                    "flows": {
                      "authorizationCode": {
                        "authorizationUrl": "https://demo.duendesoftware.com/connect/authorize",
                        "tokenUrl": "https://demo.duendesoftware.com/connect/token",
                        "scopes": {
                          "openid": "openid",
                          "profile": "profile",
                          "email": "email",
                          "api": "api",
                          "offline_access": "offline_access"
                        }
                      }
                    }
                  }
                }
              }
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi(o =>
        {
            o.AddIdentityServer("https://demo.duendesoftware.com", ["openid", "profile", "email", "api", "offline_access"]);
        });
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task AddAuth0()
    {
        var expected = """
            {
              "openapi": "3.0.1",
              "info": {
                "title": "SwaggerUI.OpenApi.Tests | v1",
                "version": "1.0.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": { },
              "components": {
                "securitySchemes": {
                  "Bearer": {
                    "type": "oauth2",
                    "flows": {
                      "authorizationCode": {
                        "authorizationUrl": "https://dev-8y4ruiprjbfy2krb.eu.auth0.com/authorize?audience=http%3a%2f%2flocalhost%3a5148%2fswagger%2f",
                        "tokenUrl": "https://dev-8y4ruiprjbfy2krb.eu.auth0.com/oauth/token",
                        "scopes": {
                          "openid": "openid",
                          "profile": "profile",
                          "email": "email",
                          "api": "api",
                          "offline_access": "offline_access"
                        }
                      }
                    }
                  }
                }
              }
            }
            """;
        expected = expected.Replace("\r\n", "\n");

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Services.AddOpenApi(o =>
        {
            o.AddAuth0("https://dev-8y4ruiprjbfy2krb.eu.auth0.com", "http://localhost:5148/swagger/",
                ["openid", "profile", "email", "api", "offline_access"]);
        });
        builder.Services.AddSwaggerUI();

        var app = builder.Build();
        app.MapOpenApi();
        app.MapSwaggerUI();

        app.Start();
        var client = app.GetTestClient();

        var actual = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);

        Assert.Equal(expected, actual);
    }
}
