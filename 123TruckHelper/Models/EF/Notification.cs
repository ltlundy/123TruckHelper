using _123TruckHelper.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123TruckHelper.Models.EF
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public Truck Truck { get; set; }

        public Load Load { get; set; }

        public NotificationStatus Status { get; set; }

        /// <summary>
        /// For soft deletes
        /// </summary>
        public bool Inactive { get; set; } = false;

        /// <summary>
        /// Distance to the pickup
        /// </summary>
        public decimal Mileage { get; set; }
        
        /// <summary>
        /// Profit of the load suggested by this notification (for this particular trucker)
        /// </summary>
        public decimal Profit { get; set; }
    }
}
