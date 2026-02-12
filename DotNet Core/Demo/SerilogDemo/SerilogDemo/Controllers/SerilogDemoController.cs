using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/serilog")]
public class SerilogDemoController : ControllerBase
{
    private readonly ILogger<SerilogDemoController> _logger;

    public SerilogDemoController(ILogger<SerilogDemoController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Test()
    {
        _logger.LogInformation("User {UserId} logged in", 101);

        _logger.LogWarning("Low stock for product {ProductId}", 500);

        try
        {
            throw new Exception("Demo Exception");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing order {OrderId}", 1001);
        }

        return Ok("Serilog Working");
    }
}


