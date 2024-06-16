using Microsoft.AspNetCore.Builder;

namespace SwaggerUi.OpenApi.Tests;

public class SwaggerUiOptionsTests
{
    [Fact]
    public void ConfigUrl()
    {
        var expected =
            """
            "configUrl":"/configs/urls.yaml"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUiOptions
        {
            ConfigUrl = "/configs/urls.yaml"
        });

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Urls()
    {
        var expected =
            """
            "urls":[{"url":"/openapi/doc1.json","name":"name1"},{"url":"/openapi/doc2.json","name":"name2"}]
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUiOptions
        {
            Urls = [new UrlDescriptor {
                Name = "name1",
                Url = "/openapi/doc1.json"
            }, new UrlDescriptor{
                Name = "name2",
                Url = "/openapi/doc2.json"
            }]
        });

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void PrimaryUrl()
    {
        var expected =
            """
            "urls.primaryName":"name2"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUiOptions
        {
            Urls = [new UrlDescriptor {
                Name = "name1",
                Url = "/openapi/doc1.json"
            }, new UrlDescriptor{
                Name = "name2",
                Url = "/openapi/doc2.json"
            }],
            PrimaryUrl = "name2"
        });

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void QueryConfigEnabled()
    {
        var expected =
            """
            "queryConfigEnabled":true
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUiOptions
        {
            QueryConfigEnabled = true,
        });

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Layout()
    {
        var expected =
            """
            "layout":"BaseLayout"
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUiOptions
        {
            Layout = "BaseLayout",
        });

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Plugins()
    {
        var expected =
            """
            configObject.plugins = [SwaggerUIBundle.plugins.Auth,SwaggerUIBundle.plugins.Configs];
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUiOptions
        {
            Plugins = ["SwaggerUIBundle.plugins.Auth", "SwaggerUIBundle.plugins.Configs"],
        });

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Presets()
    {
        var expected =
            """
            configObject.presets = [SwaggerUIStandalonePreset];
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUiOptions
        {
            Presets = ["SwaggerUIStandalonePreset"],
        });

        Assert.Contains(expected, actual);
    }
}
