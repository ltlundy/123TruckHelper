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
            var isTruck = root.TryGetProperty("truckId", out _);
            var isLoad = root.TryGetProperty("loadId", out _);
            var type = root.GetProperty("type").GetString();

            try
            {
                if (isTruck)
                {
                    await ParseAndSaveTruck(json);
                }
                else if (isLoad)
                {
                    await ParseAndSaveLoad(json);
                }
                else if (type.Equals("Start"))
                {
                    await HandleDayStart();
                }
                else if (type.Equals("End"))
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
            await DeleteAllData();
        }

        private async Task HandleDayEnd()
        {
            await DeleteAllData();
        }

        private async Task DeleteAllData()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            foreach (var load in dbContext.Loads)
            {
                dbContext.Loads.Remove(load);
            }

            foreach (var truck in dbContext.Trucks)
            {
                dbContext.Trucks.Remove(truck);
            }

            foreach (var notification in dbContext.Notifications)
            {
                dbContext.Notifications.Remove(notification);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
