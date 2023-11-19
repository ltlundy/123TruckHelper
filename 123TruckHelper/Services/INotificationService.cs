using _123TruckHelper.Models.API;

namespace _123TruckHelper.Services
{
    public interface INotificationService
    {
        Task<List<NotificationResponse>> GetAllNotificationsAsync();

        Task<int> RespondToNotificationAsync(int notificationId, bool accepted);
        
        /// <summary>
        /// Send each load to the 5 available truckers for whom it is the most profitable
        /// Will be called at some interval
        /// </summary>
        Task NotifyOfAvailableLoadsAsync();
    }
}
