using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace _123TruckHelper.Models.EF
{
    public class Load
    {
        /// <summary>
        /// Our own internal Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// This is the ID provided by 123LB.
        /// </summary>
        public int LoadId { get; set; }

        public double OriginLatitude { get; set; }

        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        public EquipType EquipmentType { get; set; }

        public decimal Price { get; set; }

        public decimal Mileage { get; set; }

        /// <summary>
        /// For soft deletes
        /// </summary>
        public bool Inactive { get; set; } = false;

        public bool IsAvailable { get; set; } = false;

        /// <summary>
        /// Not officially a foreign key, but fine right now. We were having some issues.
        /// </summary>
        public int TruckId { get; set; }
    }
}
