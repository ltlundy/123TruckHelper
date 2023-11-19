namespace _123TruckHelper.Models.API
{
    public class NotificationTruckResponse
    {
        public int NotificationID { get; set; }

        public decimal Revenue { get; set; }

        public decimal Profit { get; set; }

        public double OrigLat { get; set; }

        public double OrigLon { get; set; }

        public double DestLat { get; set; }

        public double DestLon { get; set; }

        public decimal TripDist { get; set; }

        public decimal DistToStart { get; set; }

    }
}
