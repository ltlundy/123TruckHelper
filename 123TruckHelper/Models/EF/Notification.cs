using _123TruckHelper.Enums;
using System;
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
    }
}
