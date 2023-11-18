using _123TruckHelper.Models.EF;
using _123TruckHelper.Utilities;
using Microsoft.EntityFrameworkCore;
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

        public async Task ParseAndSaveMessage(string json)
        {
            var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            // if it has a truckId, it's a truck
            var isTruck = root.TryGetProperty("truckId", out _);

            try
            {
                if (isTruck)
                {
                    await ParseAndSaveTruck(json);
                }
                else
                {
                    await ParseAndSaveLoad(json);
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

            var existing_truck = await dbContext.Trucks.Where(t => t.TruckId == truck.TruckId).SingleOrDefaultAsync();

            if (existing_truck != null)
            {
                if (!existing_truck.Busy)
                {
                    existing_truck.PositionLatitude = truck.PositionLatitude;
                    existing_truck.PositionLongitude = truck.PositionLongitude;
                    existing_truck.NextTripLengthPreference = truck.NextTripLengthPreference;
                }
            }
            else
            {
                await dbContext.Trucks.AddAsync(truck);
            }
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
    }
}
