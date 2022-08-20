using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Demo.Api.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo.Api.Services;

public interface IDemoService
{
    Task<string> Run(CancellationToken cancellationToken = default);
}

public class DemoService : IDemoService
{
    private readonly ILogger<DemoController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _endpoint;

    public DemoService(
        ILogger<DemoController> logger,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration
    )
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;

        _endpoint = configuration.GetValue<string>("EXTERNAL_API_ENDPOINT");
    }

    public async Task<string> Run(CancellationToken cancellationToken = default)
    {
        using var httpClient = _httpClientFactory.CreateClient(nameof(DemoService));

        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            _endpoint
        );

        var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);

        var statusCode = response.StatusCode;

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        _logger.LogInformation($"[SERVICE] {statusCode} {content}");

        return content;
    }
}
