using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123TruckHelper.Models.EF
{
    public class Truck
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
        public int TruckId { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public EquipType EquipType { get; set; }

        public TripLength TripLengthPreference { get; set; }

        /// <summary>
        /// Whether the trucker is currently carrying a load
        /// </summary>
        public bool Busy { get; set; }
    }
}
