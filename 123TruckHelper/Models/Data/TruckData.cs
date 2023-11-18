namespace _123TruckHelper.Models.Data
{
    public class TruckData
    {
        public int TruckId { get; set; }

        public double PositionLatitude { get; set; }

        public double PositionLongitude { get; set; }

        public EquipType EquipType { get; set; }

        public TripLength NextTripLengthPreference { get; set; }

        /// <summary>
        /// Whether the trucker is currently carrying a load
        /// </summary>
        public bool Busy { get; set; } = false;
    }
}
