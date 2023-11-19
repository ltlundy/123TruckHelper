using _123TruckHelper.Models.API;
using _123TruckHelper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Transactions;

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

        [HttpGet("truck/{truckId}")]
        public async Task GetNotificationsForTruckerAsync(int truckId)
        {

        }


        [HttpPost("/respond/{notificationId}/{accepted}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        [ProducesResponseType(410)]
        public async Task<HttpStatusCode> RespondToNotificationAsync(int notificationId, bool accepted)
        {
            var status = await _notificationService.RespondToNotificationAsync(notificationId, accepted);

            return status switch
            {
                202 => HttpStatusCode.Accepted,
                404 => HttpStatusCode.NotFound,
                410 => HttpStatusCode.Gone,
                _ => HttpStatusCode.BadRequest,
            };
        }
    }
}
