using _123TruckHelper.Models.Data;
using _123TruckHelper.Models.EF;
using Microsoft.EntityFrameworkCore;

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

            var l = await dbContext.Loads.Where(l => l.LoadId == data.LoadId).SingleOrDefaultAsync();
            if (l == null)
            {
                var load = new Load
                {
                    LoadId = data.LoadId,
                    DestinationLatitude = data.DestinationLatitude,
                    DestinationLongitude = data.DestinationLongitude,
                    OriginLatitude = data.OriginLatitude,
                    OriginLongitude = data.OriginLongitude,
                    EquipmentType = data.EquipmentType,
                    Mileage = data.Mileage,
                    Price = data.Price,
                    IsAvailable = true,
                    Inactive = false
                };
                await dbContext.Loads.AddAsync(load);
            }
            else
            {
                l.LoadId = data.LoadId;
                l.DestinationLatitude = data.DestinationLatitude;
                l.DestinationLongitude = data.DestinationLongitude;
                l.OriginLatitude = data.OriginLatitude;
                l.OriginLongitude = data.OriginLongitude;
                l.EquipmentType = data.EquipmentType;
                l.Mileage = data.Mileage;
                l.Price = data.Price;
                l.Inactive = false;
            }
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
