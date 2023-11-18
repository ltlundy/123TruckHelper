using _123TruckHelper.Models.API;

namespace _123TruckHelper.Services
{
    public interface INotificationService
    {
        Task<List<NotificationResponse>> GetAllNotificationsAsync();
    }
}
