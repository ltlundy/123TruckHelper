using _123TruckHelper.Models.Data;
using _123TruckHelper.Models.EF;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
