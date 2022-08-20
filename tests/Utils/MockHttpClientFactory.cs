using RichardSzalay.MockHttp;

namespace Demo.Api.Tests.Utils;


internal class MockHttpClientFactory : IHttpClientFactory
{
    private static MockHttpMessageHandler _mockHttpMessageHandler;

    public static void Set(MockHttpMessageHandler httpClient)
        => _mockHttpMessageHandler = httpClient;

    public HttpClient CreateClient(string name)
    {
        ArgumentNullException.ThrowIfNull(_mockHttpMessageHandler);

        return _mockHttpMessageHandler.ToHttpClient();
    }
}
