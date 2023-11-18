using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _123TruckHelper.Models.EF
{
    public class Truck
    {
        /// <summary>
        /// This is the ID provided by 123LB
        /// </summary>
        [Key]
        public int TruckId { get; set; }

        public double PositionLatitude { get; set; }

        public double PositionLongitude { get; set; }

        public EquipType EquipType { get; set; }

        public TripLength NextTripLengthPreference { get; set; }

        /// <summary>
        /// Whether the trucker is currently carrying a load
        /// </summary>
        public bool Busy { get; set; }
    }
}
