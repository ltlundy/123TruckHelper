using _123TruckHelper.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("/PhoneNumber/{phoneNumber}")]
        public async Task AddPhoneNumber(string phoneNumber)
        {

        }
    }
}
