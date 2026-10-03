using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Time.Testing;

namespace SwaggerUI.OpenApi.Versioning.Tests;

public class VersioningTests
{
    [Fact]
    public async Task VersioningTest()
    {
        // Arrange
        var expectedDocumentV1 = $$$"""
            {
              "openapi": "3.1.1",
              "info": {
                "title": "SwaggerUI.OpenApi.Versioning.Tests | v1",
                "description": "",
                "version": "1.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": {
                "/v1": {
                  "get": {
                    "tags": [
                      "SwaggerUI.OpenApi.Versioning.Tests"
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
              "tags": [
                {
                  "name": "SwaggerUI.OpenApi.Versioning.Tests"
                }
              ]
            }
            """;
        var expectedDocumentV2 = $$$"""
            {
              "openapi": "3.1.1",
              "info": {
                "title": "SwaggerUI.OpenApi.Versioning.Tests | v2",
                "description": "",
                "version": "2.0"
              },
              "servers": [
                {
                  "url": "http://localhost/"
                }
              ],
              "paths": {
                "/v2": {
                  "get": {
                    "tags": [
                      "SwaggerUI.OpenApi.Versioning.Tests"
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
              "tags": [
                {
                  "name": "SwaggerUI.OpenApi.Versioning.Tests"
                }
              ]
            }
            """;
        expectedDocumentV1 = expectedDocumentV1.Replace("\r\n", "\n");
        expectedDocumentV2 = expectedDocumentV2.Replace("\r\n", "\n");

        var now = new DateTimeOffset(2025, 04, 23, 18, 41, 23, TimeSpan.Zero);
        var fakeTimeProvider = new FakeTimeProvider(now);
        DataTypeSchemaTransformer.TimeProvider = fakeTimeProvider;

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseTestServer();
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

        var versionedApi = app.NewVersionedApi();
        versionedApi.MapGet("/v1", () => "v1!").HasApiVersion(new ApiVersion(1, 0));
        versionedApi.MapGet("/v2", () => "v2!").HasApiVersion(new ApiVersion(2, 0));

        app.Start();
        var client = app.GetTestClient();

        // Act
        var documentV1 = await client.GetStringAsync("openapi/v1.json", TestContext.Current.CancellationToken);
        var documentV2 = await client.GetStringAsync("openapi/v2.json", TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(expectedDocumentV1, documentV1);
        Assert.Equal(expectedDocumentV2, documentV2);
    }
}