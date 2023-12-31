﻿using _123TruckHelper.Models.API;
using _123TruckHelper.Models.Data;
using _123TruckHelper.Models.EF;

namespace _123TruckHelper.Utilities
{
    public static class ConversionExtensions
    {
        public static NotificationResponse Convert(this Notification notification)
        {
            return new NotificationResponse { 
                Timestamp = notification.Timestamp,
                TruckId = notification.Truck.TruckId,
                LoadId = notification.Load.LoadId,
                DestLat = notification.Load.DestinationLatitude,
                DestLon = notification.Load.DestinationLongitude,
                OrigLat = notification.Load.OriginLatitude,
                OrigLon = notification.Load.OriginLongitude,
                Profit = notification.Profit,
                DistToStart = notification.Mileage,
                Revenue = notification.Load.Price,
                TripDist = notification.Load.Mileage,
                NotificationID = notification.Id
            };
        }

        public static LoadData Convert(this Load load)
        {
            return new LoadData { 
                LoadId = load.LoadId,
                OriginLatitude = load.OriginLatitude,
                OriginLongitude = load.OriginLongitude,
                DestinationLatitude = load.DestinationLatitude,
                DestinationLongitude = load.DestinationLongitude,
                EquipmentType = load.EquipmentType,
                Price = load.Price,
                Mileage = load.Mileage,
            };
        }
    }
}
