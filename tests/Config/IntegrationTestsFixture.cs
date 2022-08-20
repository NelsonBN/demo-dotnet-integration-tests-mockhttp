using System.Net.Http.Headers;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Api.Tests.Config;


[CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture> { }


public class IntegrationTestsFixture : IDisposable
{
    public HttpClient Client;

    public readonly WebAPIFactory<AssemblyReference> _factory;

    public IntegrationTestsFixture()
    {
        _factory = new WebAPIFactory<AssemblyReference>();

        Client = _createClient();
    }

    private HttpClient _createClient()
    {
        var clientOptions = new WebApplicationFactoryClientOptions();

        var client = _factory.CreateClient(clientOptions);
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

        return client;
    }

    public TService GetService<TService>()
        => _factory.Services.GetService<TService>();

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
            Client.Dispose();
            _factory.Dispose();
        }
    }
}
