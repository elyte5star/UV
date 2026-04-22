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

        public bool IsConnected { get; private set; }


        public string ConnectionString { get;  }

        public IMongoDatabase Database { get; private set; }

        public string DbName { get; private set; }


        public Dbms(IAppConfiguration config, ILogger<Dbms> logger)
        {
            _config = config;
            _logger = logger;
            IsConnected = false;
            ConnectionString = _config.DbConnStr;
            DbName = _config.DbName;
            ConnectToDb();
        }

        public void ConnectToDb()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                _logger.LogError("Database connection string is not provided.");
                IsConnected = false;
                return;
            }
            try
            {
                var client = new MongoClient(ConnectionString);
                Database = client.GetDatabase(_config.DbName);
                Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").GetAwaiter().GetResult();
                IsConnected = true;
                _logger.LogInformation($"Successfully connected to the database: {_config.DbName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to the database");
                IsConnected = false;
            }
        }


        public IAppConfiguration GetConfig()
        {
            return _config;
        }
    }

}