using System;
using System.Text;
using andead.netcore.mqtt.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;

namespace andead.netcore.mqtt
{
    public class Startup
    {
        private const string MQTT_SERVER_KEY_NAME = "mqtt-server";
        private const string MQTT_TOPIC_KEY_NAME = "mqtt-topic";

        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvcWithDefaultRoute();

            string mqttServer = _configuration.GetValue<string>(MQTT_SERVER_KEY_NAME, String.Empty);
            string mqttTopic = _configuration.GetValue<string>(MQTT_TOPIC_KEY_NAME, String.Empty);

            if (String.IsNullOrEmpty(mqttServer) || String.IsNullOrEmpty(mqttTopic))
            {
                _logger.LogCritical(JsonConvert.SerializeObject(
                    new
                    {
                        error = "MQTT Server or MQTT Topic are not set!"
                    }
                ));

                return;
            }

            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithWebSocketServer(mqttServer)
                    .WithTls().Build())
                .Build();

            var mqttClient = new MqttFactory().CreateManagedMqttClient();
            await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic(mqttTopic).Build());

            MqttManager mqttManager = new MqttManager(_logger);
            mqttClient.UseApplicationMessageReceivedHandler((e) => {
                mqttManager.MessageReceived(e);
            });

            await mqttClient.StartAsync(options);
        }
    }
}
