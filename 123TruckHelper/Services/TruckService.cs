using _123TruckHelper.Models.Data;
using _123TruckHelper.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace _123TruckHelper.Services
{
    public class TruckService : ITruckService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TruckService(IServiceScopeFactory serviceScopeFactory) { 
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<TruckData> GetTruckLocationAsync(int truckID)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var truck = await dbContext.Trucks.Where(t => t.TruckId == truckID).SingleAsync();

            var truckData = new TruckData
            {
                PositionLatitude = truck.PositionLatitude,
                PositionLongitude = truck.PositionLongitude,
            };
            
            return truckData;
        }

        /// <summary>
        /// This method exists because Nick messed up something with foreign keys in db and we ended up with the wrong id
        /// </summary>
        /// <param name="internalId">Internal (not123LB) truck id</param>
        /// <returns></returns>
        public async Task<int> GetCorrectTruckId(int internalId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var truckIdToUse = await dbContext.Trucks
                .Where(t => t.Id == internalId)
                .Select(t => t.TruckId)
                .SingleOrDefaultAsync();

            return truckIdToUse;
        }

        public async Task CreateOrUpdateTruckAsync(TruckData truckData)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var existing_truck = await dbContext.Trucks.Where(t => t.TruckId == truckData.TruckId).SingleOrDefaultAsync();

            try
            {
                if (existing_truck != null)
                {
                    if (!existing_truck.Busy)
                    {
                        existing_truck.PositionLatitude = truckData.PositionLatitude;
                        existing_truck.PositionLongitude = truckData.PositionLongitude;
                        existing_truck.NextTripLengthPreference = truckData.NextTripLengthPreference;
                        existing_truck.Inactive = false;
                    }
                }
                else
                {
                    var truck = new Truck
                    {
                        TruckId = truckData.TruckId,
                        EquipType = truckData.EquipType,
                        PositionLatitude = truckData.PositionLatitude,
                        PositionLongitude = truckData.PositionLongitude,
                        NextTripLengthPreference = truckData.NextTripLengthPreference,
                        Busy = truckData.Busy
                    };
                    await dbContext.Trucks.AddAsync(truck);
                }
                await dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<bool> AddPhoneNumberToTruck(string phoneNumber, int truckId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var truck = await dbContext.Trucks.Where(t => t.TruckId == truckId).SingleOrDefaultAsync();

            if (truck == null)
            {
                return false;
            }
            else
            {
                truck.PhoneNumber = phoneNumber;
            }

            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
