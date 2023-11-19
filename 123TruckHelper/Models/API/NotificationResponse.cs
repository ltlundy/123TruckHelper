using _123TruckHelper.Enums;
using _123TruckHelper.Models.EF;
using MQTTnet.Client;

namespace _123TruckHelper.Models.API
{
    public class NotificationResponse : NotificationTruckResponse
    {
        public DateTimeOffset Timestamp { get; set; }

        public int TruckId { get; set; }

        public int LoadId { get; set; }

    }
}
