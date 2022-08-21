using RichardSzalay.MockHttp;

namespace Demo.Api.Tests.Utils;


internal class MockHttpClientFactory : IHttpClientFactory
{
    private static MockHttpMessageHandler _mockHttpMessageHandler;

    private static string _baseUri;

    public static void Set(MockHttpMessageHandler httpClient)
        => _mockHttpMessageHandler = httpClient;

    public static void SetBaseUri(string baseUri)
    {
        ArgumentNullException.ThrowIfNull(_baseUri);

        _baseUri = baseUri;
    }

    public HttpClient CreateClient(string name)
    {
        ArgumentNullException.ThrowIfNull(_mockHttpMessageHandler);

        return _mockHttpMessageHandler.ToHttpClient();
    }
}
