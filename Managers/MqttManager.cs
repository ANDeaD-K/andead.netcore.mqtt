using System;
using System.Collections.Generic;
using System.Text;
using andead.netcore.mqtt.Controllers;
using andead.netcore.mqtt.Models;
using Microsoft.Extensions.Logging;
using MQTTnet;
using Newtonsoft.Json;

namespace andead.netcore.mqtt.Managers
{
    public class MqttManager
    {
        private readonly ILogger _logger;
        private List<Message> _messages;

        public MqttManager(ILogger logger, List<Message> messages)
        {
            _logger = logger;
            _messages = messages;
        }

        public void MessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            _logger.LogWarning(Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload));

            _messages.Add(
                new Message()
                {
                    id = Guid.NewGuid(),
                    date_time = DateTime.Now,
                    user_id = 1,
                    message = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload))
                }
            );
        }
    }
}