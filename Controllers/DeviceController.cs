using andead.netcore.mqtt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace andead.netcore.mqtt.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        private readonly ILogger _logger;

        public DeviceController(ILogger<DeviceController> logger)
        {
            _logger = logger;
        }

        [HttpPost("add")]
        public IActionResult AddDevice(int userId, [FromBody] Device request)
        {
            _logger.LogWarning(JsonConvert.SerializeObject(request));

            return Ok();
        }
    }
}