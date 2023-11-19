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
                notif.Accepted = accepted;

                await dbContext.SaveChangesAsync();
            }

            return status;
        }
    }
}
