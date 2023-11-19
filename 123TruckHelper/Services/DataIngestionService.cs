using _123TruckHelper.Models.Data;
using _123TruckHelper.Models.EF;
using _123TruckHelper.Utilities;
using System.Text.Json;

namespace _123TruckHelper.Services
{
    public class DataIngestionService : IDataIngestionService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITruckService _truckService;
        private readonly ILoadService _loadService;
        private readonly INotificationService _notificationService;

        public DataIngestionService(IServiceScopeFactory serviceScopeFactory, ITruckService truckService, ILoadService loadService,
            INotificationService notificationService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _truckService = truckService;
            _loadService = loadService;
            _notificationService = notificationService;
        }

        public async Task ParseMessageAndTakeAction(string json)
        {
            var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            // if it has a truckId, it's a truck
            var type = root.GetProperty("type").GetString();

            try
            {
                if (type == "Truck")
                {
                    await HandleIncomingTruck(json);
                }
                else if (type == "Load")
                {
                    await HandleIncomingLoad(json);
                }
                else if (type == "Start")
                {
                    await HandleDayStart();
                }
                else if (type == "End")
                {
                    await HandleDayEnd();
                }
            }

            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
     
        }

        private async Task HandleIncomingTruck(string json)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Ignore case sensitivity of property names
                Converters = { new EquipTypeConverter(), new TripLengthConverter() }
            };

            var truck = JsonSerializer.Deserialize<TruckData>(json, jsonSerializerOptions);

            await _truckService.CreateOrUpdateTruckAsync(truck);
        }

        private async Task HandleIncomingLoad(string json)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new EquipTypeConverter(), new TripLengthConverter() }
            };

            var load = JsonSerializer.Deserialize<LoadData>(json, jsonSerializerOptions);

            await _loadService.AddLoadAsync(load);
        }

        private async Task HandleDayStart()
        {
            await MarkDataInactive();
        }

        private async Task HandleDayEnd()
        {
            await MarkDataInactive();
        }

        private async Task MarkDataInactive()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            foreach (var load in dbContext.Loads)
            {
                load.Inactive = true;
            }

            foreach (var truck in dbContext.Trucks)
            {
                truck.Inactive = true;
            }

            foreach (var notification in dbContext.Notifications)
            {
                notification.Inactive = true;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
