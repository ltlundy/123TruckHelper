using _123TruckHelper.Models.Data;
using _123TruckHelper.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;

namespace _123TruckHelper.Services
{
    public class LoadService : ILoadService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public LoadService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task AddLoadAsync(LoadData data)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var load = new Load
            {
                LoadId = data.LoadId,
                DestinationLatitude = data.DestinationLatitude,
                DestinationLongitude = data.DestinationLongitude,
                OriginLatitude = data.OriginLatitude,
                OriginLongitude = data.OriginLongitude,
                EquipmentType = data.EquipmentType,
                Mileage = data.Mileage,
                Price = data.Price
            };

            await dbContext.Loads.AddAsync(load);
            await dbContext.SaveChangesAsync();
        }

        public async Task<int> ClaimLoad(int loadId, int truckId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TruckHelperDbContext>();

            var load = await dbContext.Loads.Where(l => l.LoadId == loadId).SingleOrDefaultAsync();
            var truck = await dbContext.Trucks.Where(t => t.TruckId == truckId).SingleOrDefaultAsync();

            if (load == null || truck == null)
            {
                return 404;
            } 
            else if (!load.IsAvaiable)
            {
                return 410;
            }
            else
            {
                load.IsAvaiable = false;
                truck.Busy = true;
                load.Truck = truck;
                dbContext.SaveChanges();
                return 202;
            }

        }
    }
}
