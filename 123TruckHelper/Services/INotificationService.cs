using _123TruckHelper.Models.API;

namespace _123TruckHelper.Services
{
    public interface INotificationService
    {
        Task<List<NotificationResponse>> GetAllNotificationsAsync();

        /// <summary>
        /// Given a load, keep notifying drivers of it until someone accepts or we try everyone
        /// </summary>
        /// <returns></returns>
        Task NotifyEligibleDriversAsync(int loadId);
    }
}
