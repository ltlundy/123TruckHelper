namespace _123TruckHelper.Models.API
{
    public class NotificationCollection : HttpResponseMessage
    {
        
        public IEnumerable<NotificationTruckResponse> NotificationTruckResponses { get; set; }

        public double CurrLat { get; set; }

        public double CurrLon { get; set; }
    }
}
