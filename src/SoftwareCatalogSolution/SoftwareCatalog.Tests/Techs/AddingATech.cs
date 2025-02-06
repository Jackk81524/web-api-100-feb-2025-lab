namespace SoftwareCatalog.Tests.Techs;
using Alba;
using Alba.Security;
using SoftwareCatalog.Api.Techs;
using System.Security.Claims;

[Trait("Category", "System")]
public class AddingATech
{
    [Fact]
    public async Task CanAddATech()
    {
        var fakeIdentity = new AuthenticationStub().WithName("X00001")
            .With(new Claim(ClaimTypes.Role, "manager"));

        var host = await AlbaHost.For<Program>(fakeIdentity);

        var requestModel = new TechCreateModel
        {
            Name = "John Doe",
            Email = "jdoe@mail.com",
            Phone = "516-123-4567"
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(201);
        });
    }

    [Theory]
    [InlineData("", null)]
    [InlineData(null, null)]
    [InlineData(null, "")]
    [InlineData("", "")]
    public async Task PhoneEmailAreNotValidated(string? email, string? phone)
    {
        var fakeIdentity = new AuthenticationStub().WithName("X00001")
            .With(new Claim(ClaimTypes.Role, "manager"));

        var host = await AlbaHost.For<Program>(fakeIdentity);

        var requestModel = new TechCreateModel
        {
            Name = "John Doe",
            Email = email,
            Phone = phone
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(400);
        });
    }

    [Theory]
    [InlineData("test@mail.com", "")]
    [InlineData("test@mail.com", null)]
    [InlineData("", "1234567")]
    [InlineData(null, "1234567")]
    public async Task PhoneEmailAreValidated(string? email, string? phone)
    {
        var fakeIdentity = new AuthenticationStub().WithName("X00001")
            .With(new Claim(ClaimTypes.Role, "manager"));

        var host = await AlbaHost.For<Program>(fakeIdentity);

        var requestModel = new TechCreateModel
        {
            Name = "John Doe",
            Email = email,
            Phone = phone
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(201);
        });
    }


    [Fact]
    public async Task NotAuthurized()
    {
        var host = await AlbaHost.For<Program>();

        var requestModel = new TechCreateModel
        {
            Name = "John Doe",
            Email = "Test@mail.com",
            Phone = "123-123-1234"
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(401);
        });
    }
}

