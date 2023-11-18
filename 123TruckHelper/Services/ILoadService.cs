using _123TruckHelper.Models.Data;

namespace _123TruckHelper.Services
{
    public interface ILoadService
    {
        Task AddLoadAsync(LoadData data);

        /// <summary>
        /// Use the 123lb load ID to get a load
        /// </summary>
        /// <param name="loadId"></param>
        /// <returns>The load we found</returns>
        Task<LoadData> GetLoadAsync(int loadId);
    }
}
