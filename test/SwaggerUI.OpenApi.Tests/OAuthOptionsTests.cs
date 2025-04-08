using Microsoft.AspNetCore.Builder;

namespace SwaggerUI.OpenApi.Tests;

public class OAuthOptionsTests
{
    [Fact]
    public void ClientId()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"clientId":"myClientId"}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                ClientId = "myClientId"
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ClientSecret()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"clientSecret":"myClientSecret"}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                ClientSecret = "myClientSecret"
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Realm()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"realm":"myRealm"}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                Realm = "myRealm"
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void AppName()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"appName":"myAppName"}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                AppName = "myAppName"
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void ScopeSeparator()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"scopeSeparator":","}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                ScopeSeparator = ","
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void Scopes()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"scopes":["email","offline_access"]}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                Scopes = ["email", "offline_access"]
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void AdditionalQueryStringParams()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"additionalQueryStringParams":{"key1":"value1","key2":"value2"}}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                AdditionalQueryStringParams = new Dictionary<string, string>
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                }
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void UseBasicAuthenticationWithAccessCodeGrant()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"useBasicAuthenticationWithAccessCodeGrant":true}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                UseBasicAuthenticationWithAccessCodeGrant = true
            }
        }, []);

        Assert.Contains(expected, actual);
    }

    [Fact]
    public void UsePkceWithAuthorizationCodeGrant()
    {
        var expected =
            """
            var oauthConfigObject = JSON.parse('{"usePkceWithAuthorizationCodeGrant":true}');
            """;

        var actual = Endpoints.GetIndexCore("My Document", new SwaggerUIOptions
        {
            OAuthOptions = new OAuthOptions
            {
                UsePkceWithAuthorizationCodeGrant = true
            }
        }, []);

        Assert.Contains(expected, actual);
    }
}
