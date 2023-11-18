using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;

namespace _123TruckHelper.Controllers
{
    public class MQTTListener
    {
        //public static async Task Main(string[] args)
        //{
        //    await Handle_Received_Application_Message();
        //}

        public static async Task Handle_Received_Application_Message()
        {
            /*
             * This sample subscribes to a topic and processes the received message.
             */

            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                   .WithTcpServer("fortuitous-welder.cloudmqtt.com", 1883)
                   .WithClientId("(TEAMNAME)")
                   .WithCredentials("CodeJamUser", "123CodeJam")
                   .WithWillQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                   .WithCleanSession(true)
                   .Build();

                // Setup message handling before connecting so that queued messages
                // are also handled properly. When there is no event handler attached all
                // received messages get lost.
                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    Console.WriteLine("Received application message.");

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

                Console.WriteLine("MQTT client subscribed to topic.");
            }
        }

    }
}
