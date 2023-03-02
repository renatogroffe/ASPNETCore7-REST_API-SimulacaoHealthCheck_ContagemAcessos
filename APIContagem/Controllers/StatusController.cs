using System.Net;
using Microsoft.AspNetCore.Mvc;
using APIContagem.Models;

namespace APIContagem.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase
{
    private static bool _healthy = true;
    private readonly ILogger<StatusController> _logger;

    public StatusController(ILogger<StatusController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<StatusApi> Get()
    {
        var status = new StatusApi
        {
            Healthy = _healthy,
            Mensagem = _healthy ? "OK" : "Unhealthy"
        };

        if (_healthy)
        {
            _logger.LogInformation("Simulacao status = OK");
            return status;
        }
        else
        {
            _logger.LogError("Simulacao status = Unhealthy");
            return new ObjectResult(status)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

    [HttpGet("healthy")]
    public IActionResult SetHealthy()
    {
        _healthy = true;
        _logger.LogInformation("Novo status = Healthy");
        return Ok();
    }

    [HttpGet("unhealthy")]
    public IActionResult SetUnhealthy()
    {
        _healthy = false;
        _logger.LogWarning("Novo status = Unhealthy");
        return Ok();
    }
}