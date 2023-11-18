using _123TruckHelper.Models.Data;

namespace _123TruckHelper.Services
{
    public interface ILoadService
    {
        Task AddLoadAsync(LoadData data);
    }
}
