using System.Threading;
using System.Threading.Tasks;
using Demo.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.Api.Controllers;

[ApiController]
[Route("demo")]
public class DemoController : ControllerBase
{
    private readonly ILogger<DemoController> _logger;
    private readonly IDemoService _service;

    public DemoController(
        ILogger<DemoController> logger,
        IDemoService service
    )
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"[CONTROLLER]");

        var resposne = await _service.Run(cancellationToken);

        return Ok(resposne);
    }
}
