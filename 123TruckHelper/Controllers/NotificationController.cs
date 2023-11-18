using _123TruckHelper.Models.API;
using _123TruckHelper.Services;
using Microsoft.AspNetCore.Mvc;

namespace _123TruckHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService) 
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get all notifications for the admin view
        /// </summary>
        /// <returns>A list of all notifications sent out</returns>
        [HttpGet("/all")]
        public async Task<List<NotificationResponse>> GetAllNotificationsAsync()
        {
            return await _notificationService.GetAllNotificationsAsync();
        }

        public async Task RespondToNotificationAsync(int notificationId, bool accepted)
        {
            await _notificationService.RespondToNotificationAsync(notificationId, accepted);
        }
    }
}
