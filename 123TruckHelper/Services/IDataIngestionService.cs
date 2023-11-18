using _123TruckHelper.Models.EF;

namespace _123TruckHelper.Services
{
    public interface IDataIngestionService
    {
        Task ParseMessageAndTakeAction(String json);
    }
}
