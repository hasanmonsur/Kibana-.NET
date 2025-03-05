using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogsKibanaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("This is an informational log.");
            _logger.LogWarning("This is a warning log.");
            _logger.LogError("This is an error log.");

            return Ok("Success .....");
        }

    }
}
