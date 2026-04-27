using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using WebAPI.Domain.Interfaces;
namespace WebAPI.Data.DbContext

{

    public class Dbms
    {
        private readonly IAppConfiguration _config;

        private readonly ILogger<Dbms> _logger;

        private MongoClient _client;

        public string ConnectionString { get; }

        public IMongoDatabase Database { get; private set; }

        public string DbName { get; private set; }


        public Dbms(IAppConfiguration config, ILogger<Dbms> logger)
        {
            _config = config;
            _logger = logger;
            ConnectionString = _config.DbConnStr;
            DbName = _config.DbName;

        }

        public async Task ConnectToDb()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                _logger.LogError("Database connection string is not provided.");

                return;
            }
            try
            {
                _client = new MongoClient(ConnectionString);
                Database = _client.GetDatabase(DbName);
                await Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
                _logger.LogInformation($"Successfully connected to the database: {DbName}");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to the database");

            }
            await Task.Delay(_config.WaitTime);
        }

    }

}