using _123TruckHelper.Enums;
using _123TruckHelper.Models.API;
using _123TruckHelper.Models.EF;
using _123TruckHelper.Utilities;
using Microsoft.EntityFrameworkCore;

namespace _123TruckHelper.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILoadService _loadService;

        private const decimal GAS_PRICE_PER_MILE = 1.38M;

        public NotificationService(IServiceScopeFactory serviceScopeFactory, ILoadService loadService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _loadService = loadService;
        }

        public async Task<List<NotificationResponse>> GetAllNotificationsAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            return await dbContext.Notifications
                .Select(n => n.Convert())
                .ToListAsync();
        }

        public async Task GetNotificationsForTruckIDAsync(int truckID)
        {

        }

        public async Task<int> RespondToNotificationAsync(int notificationId, bool accepted)
        {

            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var notif = await dbContext.Notifications
                .Where(n => n.Id == notificationId)
                .Include(n => n.Load)
                .Include(n => n.Truck)
                .SingleOrDefaultAsync();

            if (notif == null)
            {
                return 404;
            }

            var status = await _loadService.ClaimLoad(notif.Load.LoadId, notif.Truck.TruckId);

            if (status == 202)
            {
                notif.Status = NotificationStatus.Accepted;

                await dbContext.SaveChangesAsync();
            }

            return status;
        }
        
        /// <inheritdoc/>
        public async Task NotifyOfAvailableLoadsAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var availableActiveLoads = await dbContext
                .Loads
                .Where(l => !l.Inactive)
                .Where(l => l.IsAvailable)
                .ToListAsync();

            foreach (var load in availableActiveLoads)
            {
                var isShort = load.Mileage < 200;

                // megaquery
                // TODO: ignore loads a ridiculous distance
                var toNotify = await dbContext.Trucks
                    .Where(t => !t.Busy)
                    .Where(t => (t.NextTripLengthPreference == TripLength.Short) == isShort)
                    .Where(t => t.EquipType == load.EquipmentType)
                    .Where(t => GetNumActiveNotifications(t.TruckId) < 5)
                    .OrderByDescending(t => CalculateProfit(t.TruckId, load.LoadId))
                    .Take(5)
                    .ToListAsync();

                foreach (var truck in toNotify)
                {
                    var notification = new Notification {
                        Timestamp = DateTimeOffset.Now,
                        Truck = truck,
                        Load = load,
                        Status = NotificationStatus.Sent
                    };

                    await dbContext.Notifications.AddAsync(notification);
                }

                await dbContext.SaveChangesAsync();
            }
        }

        private void NotifyOfLoad(Truck truck, Load load)
        {
            
        }

        /// <summary>
        /// Calculate the profit for this guy if he takes the load
        /// Purposely not making it async so we can call in lambda
        /// </summary>
        /// <param name="truckId">Truck ID</param>
        /// <param name="loadId">Load ID</param>
        /// <returns></returns>
        private decimal CalculateProfit(int truckId, int loadId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var truck = dbContext.Trucks
                .Where(t => t.TruckId == truckId)
                .SingleOrDefault();

            var load = dbContext.Loads
                .Where(l => l.LoadId == loadId)
                .SingleOrDefault();

            var gasCostCarryingLoad = load.Mileage * GAS_PRICE_PER_MILE;

            // TODO: Use google maps instead of as the crow flies
            var milesToOrigin = CalculateDistance(truck.PositionLatitude, truck.PositionLongitude, load.OriginLatitude, load.OriginLongitude);
            var gasCostToOrigin = Convert.ToDecimal(milesToOrigin) * GAS_PRICE_PER_MILE;
            var totalGasCost = gasCostCarryingLoad + gasCostToOrigin;

            var profit = load.Price - totalGasCost;

            return profit;
        }

        /// <summary>
        /// Get number of active notifications for a truck
        /// Purposely making this synchronous so we can call in lambda
        /// </summary>
        /// <param name="truckId"></param>
        /// <returns></returns>
        private int GetNumActiveNotifications(int truckId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var count = dbContext.Notifications
                .Where(n => n.Truck.Id == truckId)
                .Where(n => n.Status == NotificationStatus.Sent)
                .Where(n => !n.Inactive)
                .Count();

            return count;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371.0;

            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distanceKm = R * c;

            double distanceMiles = distanceKm * 0.621371;

            return distanceMiles;
        }

        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
