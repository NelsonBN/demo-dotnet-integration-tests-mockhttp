using System.Net;
using System.Net.Mime;
using Demo.Api.Tests.Config;
using Demo.Api.Tests.Utils;
using RichardSzalay.MockHttp;

namespace Demo.Api.Tests;


[Collection(nameof(IntegrationApiTestsFixtureCollection))]
public class DemoTest
{
    private readonly IntegrationTestsFixture _testsFixture;
    private readonly MockHttpMessageHandler _mockHttpHandler = new();

    public DemoTest(IntegrationTestsFixture testsFixture)
        => _testsFixture = testsFixture;



    [Fact]
    public async Task GetMethod_Request_200()
    {
        // Arrange
        // var service = _testsFixture.GetService<IStagingHttpClient>();


        var mockHttp = new MockHttpMessageHandler();
        mockHttp
            .When("*")
            .Respond(
                HttpStatusCode.OK,
                MediaTypeNames.Text.Plain,
                "fake get"
            );

        MockHttpClientFactory.Set(mockHttp);


        // Act
        var response = await _testsFixture.Client.GetAsync("/demo");
        var content = await response.Content.ReadAsStringAsync();


        // Assert
        content.Should()
            .Be("\"fake get\"");
    }

    [Fact]
    public async Task PostMethod_Request_200()
    {
        // Arrange
        //var service = _testsFixture.GetService<IStagingHttpClient>();


        var mockHttp = new MockHttpMessageHandler();

        mockHttp
            .When("*")
            .Respond(
                HttpStatusCode.Created,
                MediaTypeNames.Text.Plain,
        "fake post"
        );

        MockHttpClientFactory.Set(mockHttp);


        // Act
        var response = await _testsFixture.Client.GetAsync("/demo");
        var content = await response.Content.ReadAsStringAsync();


        // Assert
        content.Should()
            .Be("\"fake post\"");
    }
}
