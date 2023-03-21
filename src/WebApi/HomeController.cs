using GrpcService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private readonly Greeter.GreeterClient _greeterClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(Greeter.GreeterClient greeterClient, ILogger<HomeController> logger)
    {
        _greeterClient = greeterClient;
        _logger        = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string name)
    {
        _logger.LogInformation("-- init request");

        return Ok(await _greeterClient.SayHelloAsync(new HelloRequest { Name = name }));
    }
}
