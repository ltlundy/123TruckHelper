using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123TruckHelper.Models.EF
{
    public class Load
    {
        /// <summary>
        /// Our internal, auto-generated ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// This is the ID provided by 123LB. Not necessarily the same as our internal database ID
        /// </summary>
        public int LoadId { get; set; }

        public string OriginLatitude { get; set; }

        public string OriginLongitude { get; set; }

        public string DestinationLatitude { get; set; }

        public string DestinationLongitude { get; set; }

        public EquipType EquipType { get; set; }

        public decimal Price { get; set; }

        public decimal Mileage { get; set; }
    }
}
