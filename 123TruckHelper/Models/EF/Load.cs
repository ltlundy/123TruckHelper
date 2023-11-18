using System.ComponentModel.DataAnnotations;

namespace _123TruckHelper.Models.EF
{
    public class Load
    {
        /// <summary>
        /// This is the ID provided by 123LB.
        /// </summary>
        [Key]
        public int LoadId { get; set; }

        public double OriginLatitude { get; set; }

        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        public EquipType EquipmentType { get; set; }

        public decimal Price { get; set; }

        public decimal Mileage { get; set; }
    }
}
