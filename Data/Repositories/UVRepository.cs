using MongoDB.Driver;
using WebAPI.Data.Messaging;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;
using WebAPI.Data.DbContext;
using Microsoft.Extensions.Logging;

namespace WebAPI.Data.Repositories
{
    public class UVRepository: IUVRepository
    {
        private readonly IMongoCollection<MongoMQTTData> _collection;


        private readonly ILogger<UVRepository> _logger;

        public UVRepository(Dbms dbms, ILogger<UVRepository> logger)
        {
            _collection = dbms.Database.GetCollection<MongoMQTTData>("pressure");
            _logger = logger;

        }

        public async Task SaveUVData(MQTTData sensorData)
        {
            var mongoData = new MongoMQTTData
            {
                Payload = sensorData.Payload,
                MessageDate = sensorData.MessageDate,
                ClientId = sensorData.ClientId,
                Topic = sensorData.Topic
            };

            await _collection.InsertOneAsync(mongoData);

        }

        public async Task<List<MQTTData>> GetRecentData(int limit)
        {
            var result = await _collection.Find(_ => true).SortByDescending(d => d.MessageDate).Limit(limit).ToListAsync();
            return result.Select(d => new MQTTData
            {
                Topic = d.Topic,
                Payload = d.Payload,
                MessageDate = d.MessageDate,
                ClientId = d.ClientId
            }).ToList();
        }


        public async Task<List<MQTTData>> GetAllData()
        {
            var result = await _collection.Find(_ => true).ToListAsync();
            return result.Select(d => new MQTTData
            {
                Topic = d.Topic,
                Payload = d.Payload,
                MessageDate = d.MessageDate,
                ClientId = d.ClientId
            }).ToList();
        }
    }
}