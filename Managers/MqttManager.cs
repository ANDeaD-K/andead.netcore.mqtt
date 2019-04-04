using System.Text;
using Microsoft.Extensions.Logging;
using MQTTnet;

namespace andead.netcore.mqtt.Managers
{
    public class MqttManager
    {
        private readonly ILogger _logger;

        public MqttManager(ILogger logger)
        {
            _logger = logger;
        }

        public void MessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            _logger.LogWarning(Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload));
        }
    }
}