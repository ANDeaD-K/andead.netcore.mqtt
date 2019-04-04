using System;
using System.Collections.Generic;
using andead.netcore.mqtt.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace andead.netcore.mqtt.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private List<Message> _logMessages;

        public LogController(List<Message> logMessages)
        {
            _logMessages = logMessages;
        }

        [HttpGet("get")]
        public IActionResult GetLog(int userId)
        {
            return Ok(JsonConvert.SerializeObject(_logMessages));
        }
    }
}