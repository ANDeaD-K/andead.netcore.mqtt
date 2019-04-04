using System;
using andead.netcore.mqtt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;

namespace andead.netcore.mqtt.Controllers
{
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly ILogger _logger;
        private IManagedMqttClient _mqttClient;

        public DevicesController(ILogger<DevicesController> logger, IManagedMqttClient mqttClient)
        {
            _logger = logger;
            _mqttClient = mqttClient;
        }

        [HttpPost("add")]
        public IActionResult AddDevice(int userId, [FromBody] Device device)
        {
            _mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic(device.topic).Build());

            _logger.LogWarning(JsonConvert.SerializeObject(
                new
                {
                    message = "Added new device",
                    device
                }
            ));

            return Ok(JsonConvert.SerializeObject(
                new
                {
                    success = DateTime.Now
                }
            ));
        }
    }
}