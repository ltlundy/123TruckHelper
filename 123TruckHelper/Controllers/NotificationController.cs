﻿using _123TruckHelper.Models.API;
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
        private readonly ITruckService _truckService;

        public NotificationController(INotificationService notificationService, ITruckService truckService) 
        {
            _notificationService = notificationService;
            _truckService = truckService;
        }

        /// <summary>
        /// Get all notifications for the admin view
        /// </summary>
        /// <returns>A list of all notifications sent out</returns>
        [HttpGet("/all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NotificationResponse>))]
        public async Task<IEnumerable<NotificationResponse>> GetAllNotificationsAsync()
        {
            return await _notificationService.GetAllNotificationsAsync();
        }

        [HttpGet("truck/{truckId}")]
        [ProducesResponseType(200, Type = typeof(NotificationCollection))]
        public async Task<NotificationCollection> GetNotificationsForTruckerAsync(int truckId)
        {
            // nick made a mistake with fk or something and we need to get the right id (the 123LB one)
            var correctId = await _truckService.GetCorrectTruckId(truckId);

            var truckTask = _truckService.GetTruckLocationAsync(correctId);
            var notificationsTask = _notificationService.GetNotificationsForTruckIDAsync(correctId);

            var truckData = await truckTask;
            var response = new NotificationCollection
            {
                CurrLat = truckData.PositionLatitude,
                CurrLon = truckData.PositionLongitude,
                NotificationTruckResponses = await notificationsTask
            };
            return response;
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
