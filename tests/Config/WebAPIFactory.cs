using Demo.Api.Tests.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Demo.Api.Tests.Config;
public class WebAPIFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    => builder.ConfigureTestServices(services =>


        //services.RemoveAll(typeof(IHttpClientFactory));
        //services.AddSingleton<IHttpClientFactory>(token => new MockHttpClientFactory("https://google.com", "asfasf"));

        // services.AddSingleton<IStagingHttpClient, StagingHttpClient>();
        services.Override<IHttpClientFactory, MockHttpClientFactory>());
}
