using _123TruckHelper.Services;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System.Text;

namespace _123TruckHelper.Ingestion
{
    public class MQTTListener
    {
        //public static async Task Main(string[] args)
        //{
        //    await Handle_Received_Application_Message();
        //}

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
                   .WithClientId("nick-triantos")
                   .WithCredentials("CodeJamUser", "123CodeJam")
                   .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                   .WithCleanSession(true)
                   .Build();

                // Setup message handling before connecting so that queued messages
                // are also handled properly. When there is no event handler attached all
                // received messages get lost.
                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    string jsonPayload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    Console.WriteLine(jsonPayload);

                    // read in the message and save the info
                    var dataIngestionService = _serviceProvider.GetRequiredService<IDataIngestionService>();
                    dataIngestionService.ParseAndSaveMessage(jsonPayload);

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
