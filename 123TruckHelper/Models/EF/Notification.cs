﻿using System;
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

        public bool Accepted { get; set; }
    }
}
