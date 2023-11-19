using _123TruckHelper.Enums;
using _123TruckHelper.Models.API;
using _123TruckHelper.Models.EF;
using _123TruckHelper.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace _123TruckHelper.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILoadService _loadService;

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

        public async Task<IEnumerable<NotificationTruckResponse>> GetNotificationsForTruckIDAsync(int truckID)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var notificationsEF = await dbContext.Notifications
                .Include(n => n.Truck)
                .Include(n => n.Load)
                .Where(n => n.Truck.TruckId == truckID && n.Status == NotificationStatus.Sent && !n.Inactive)
                .OrderByDescending(n => n.Profit)
                .ThenBy(n => n.Mileage) // ordering by profit then distance to get there
                .ToListAsync();
            return notificationsEF.Select(notificationEF => new NotificationTruckResponse
            {
                DestLat = notificationEF.Load.DestinationLatitude,
                DestLon = notificationEF.Load.DestinationLongitude,
                OrigLat = notificationEF.Load.OriginLatitude,
                OrigLon = notificationEF.Load.OriginLongitude,
                // TODO :: Add Dist to start, profit
                Revenue = notificationEF.Load.Price,
                TripDist = notificationEF.Load.Mileage,
                NotificationID = notificationEF.Id
            }).ToList();
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
            // will be called by a running background service at some interval
            // notify people of each load to be merged, up to 3 notifs per driver
        }
    }
}
