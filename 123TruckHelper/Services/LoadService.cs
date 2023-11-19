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

            var load = await dbContext.Loads.Where(l => l.LoadId == data.LoadId).SingleOrDefaultAsync() ?? new Load();

            load.LoadId = data.LoadId;
            load.DestinationLatitude = data.DestinationLatitude;
            load.DestinationLongitude = data.DestinationLongitude;
            load.OriginLatitude = data.OriginLatitude;
            load.OriginLongitude = data.OriginLongitude;
            load.EquipmentType = data.EquipmentType;
            load.Mileage = data.Mileage;
            load.Price = data.Price;
            load.Inactive = false;
            load.IsAvailable = true;
         

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
            else if (!load.IsAvailable)
            {
                return 410;
            }
            else
            {
                load.IsAvailable = false;
                truck.Busy = true;
                load.TruckId = truck.Id;
                dbContext.SaveChanges();
                return 202;
            }

        }
    }
}
