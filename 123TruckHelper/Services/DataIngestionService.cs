using _123TruckHelper.Models.Data;
using _123TruckHelper.Models.EF;
using _123TruckHelper.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace _123TruckHelper.Services
{
    public class DataIngestionService : IDataIngestionService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITruckService _TruckService;

        public DataIngestionService(IServiceScopeFactory serviceScopeFactory, ITruckService truckService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _TruckService = truckService;
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

            var truck = JsonSerializer.Deserialize<TruckData>(json, jsonSerializerOptions);

            await _TruckService.CreateOrUpdateTruckAsync(truck);
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
