﻿using _123TruckHelper.Services;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System.Text;

namespace _123TruckHelper.Ingestion
{
    public class MQTTListener
    {
        private static IServiceProvider _serviceProvider;

        public static void InitializeServiceProvider(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Initializing MQTT.");
            _serviceProvider = serviceProvider;
            Console.WriteLine("Initialized MQTT.");
        }

        public static async Task ListenAndProcessAsync()
        {
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                   .WithTcpServer("fortuitous-welder.cloudmqtt.com", 1883)
                   .WithClientId("nick-t1122")
                   .WithCredentials("CodeJamUser", "123CodeJam")
                   .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                   .WithCleanSession(true)
                   .Build();

                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    // Console.WriteLine("Message Recieved");
                    string jsonPayload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    // Console.WriteLine(jsonPayload);

                    // read in the message and save the info
                    var dataIngestionService = _serviceProvider.GetRequiredService<IDataIngestionService>();
                    dataIngestionService.ParseMessageAndTakeAction(jsonPayload);

                    return Task.CompletedTask;
                };

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("CodeJam");
                        })
                    .Build();

                await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                await Task.Delay(Timeout.Infinite);
            }
        }

    }
}
