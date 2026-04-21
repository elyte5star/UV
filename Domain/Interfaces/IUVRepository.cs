using WebAPI.Domain.Entities;


namespace WebAPI.Domain.Interfaces
{
    public interface IUVRepository
        {

        
        Task SaveUVData(MQTTData data);
        Task<List<MQTTData>> GetRecentData(int limit);
        Task<List<MQTTData>> GetAllData();
    }
}