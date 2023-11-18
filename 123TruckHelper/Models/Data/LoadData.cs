namespace _123TruckHelper.Models.Data
{
    public class LoadData
    {
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
