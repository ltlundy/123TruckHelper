using _123TruckHelper.Models.API;
using _123TruckHelper.Models.EF;

namespace _123TruckHelper.Utilities
{
    public static class ConversionExtensions
    {
        public static NotificationResponse Convert(this Notification notification)
        {
            return new NotificationResponse { 
                Timestamp = notification.Timestamp,
                TruckId = notification.Truck.TruckId,
                LoadId = notification.Load.LoadId,
                Accepted = notification.Accepted,
            };
        }
    }
}
