using _123TruckHelper.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace _123TruckHelper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TruckerController : ControllerBase
    {
        private ITruckService TruckService { get; }

        public TruckerController(ITruckService TruckService)
        {
            this.TruckService = TruckService;
        }

        [HttpPost("/PhoneNumber/{truckId}/{phoneNumber}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        public async Task<HttpStatusCode> AddPhoneNumber(int truckId, string phoneNumber)
        {
            var result = await TruckService.AddPhoneNumberToTruck(phoneNumber, truckId);

            return (result) ? HttpStatusCode.Accepted : HttpStatusCode.NotFound;
        }
    }
}
