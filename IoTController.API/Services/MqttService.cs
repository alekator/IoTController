using MQTTnet;
using MQTTnet.Client;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MQTTnet.Packets;
using IoTController.Shared;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using IoTController.API.Hubs;
using Microsoft.AspNetCore.SignalR;


namespace IoTController.API.Services
{
    public class MqttService
    {
        private IMqttClient _mqttClient;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<DeviceDataHub> _hubContext;

        public MqttService(IServiceScopeFactory scopeFactory, IHubContext<DeviceDataHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _hubContext = hubContext;
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            _mqttClient.ConnectedAsync += OnConnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;
        }

        public async Task ConnectAsync()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithClientId("IoTDashboardServer")
                .WithTcpServer("localhost", 1883)
                .Build();

            _mqttClient.ConnectedAsync += OnConnectedAsync;
            _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;

            await _mqttClient.ConnectAsync(options, CancellationToken.None);
        }

        private async Task OnConnectedAsync(MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("Connected to MQTT Broker.");

            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic("devices/+/data")
                .Build();

            await _mqttClient.SubscribeAsync(new MqttClientSubscribeOptions
            {
                TopicFilters = new List<MqttTopicFilter> { topicFilter }
            });

            Console.WriteLine("Subscribed to topic 'devices/+/data'.");
        }

        private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            Console.WriteLine($"Received message from topic '{topic}': {payload}");

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IoTContext>();

                try
                {
                    var data = JsonConvert.DeserializeObject<DeviceData>(payload);

                    var topicParts = topic.Split('/');
                    if (topicParts.Length >= 3)
                    {
                        data.DeviceId = topicParts[1];
                    }

                    data.Timestamp = DateTime.UtcNow;

                    context.DeviceData.Add(data);
                    await context.SaveChangesAsync();

                    await _hubContext.Clients.All.SendAsync("ReceiveDeviceData", data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving data to database: {ex.Message}");
                }
            }
        }


    }
}