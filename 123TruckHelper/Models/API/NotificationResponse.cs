using _123TruckHelper.Enums;
using _123TruckHelper.Models.EF;

namespace _123TruckHelper.Models.API
{
    public class NotificationResponse
    {
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// The 123LB id of the truck this notification was sent to
        /// </summary>
        public int TruckId { get; set; }

        /// <summary>
        /// The 123LB id of the load this notification was about
        /// </summary>
        public int LoadId { get; set; }

        /// <summary>
        /// Accepted, denied, not yet responded, expired
        /// </summary>
        public NotificationStatus Status { get; set; }
    }
}
