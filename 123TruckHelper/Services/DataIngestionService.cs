using _123TruckHelper.Models.EF;
using _123TruckHelper.Utilities;
using System.Text.Json;

namespace _123TruckHelper.Services
{
    public class DataIngestionService : IDataIngestionService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DataIngestionService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
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
                    await ParseAndSaveTruck(json);
                }
                else if (type == "Load")
                {
                    await ParseAndSaveLoad(json);
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

        private async Task ParseAndSaveTruck(string json)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Ignore case sensitivity of property names
                Converters = { new EquipTypeConverter(), new TripLengthConverter() }
            };

            var truck = JsonSerializer.Deserialize<Truck>(json, jsonSerializerOptions);

            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            await dbContext.Trucks.AddAsync(truck);
            await dbContext.SaveChangesAsync();
        }

        private async Task ParseAndSaveLoad(string json)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new EquipTypeConverter(), new TripLengthConverter() }
            };

            var load = JsonSerializer.Deserialize<Load>(json, jsonSerializerOptions);

            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            await dbContext.Loads.AddAsync(load);
            await dbContext.SaveChangesAsync();
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
