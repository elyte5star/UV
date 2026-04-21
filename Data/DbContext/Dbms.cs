
using WebAPI.Data.Config;
using MongoDB.Driver;
using MongoDB.Bson;
using WebAPI.Domain.Interfaces;
namespace WebAPI.Data.DbContext

{
    
public class Dbms
    {
        private readonly IAppConfiguration _config;

        public bool IsConnected { get; private set; }
       

        public string ConnectionString { get; set; }

        public IMongoDatabase Database { get; private set; }

        public string DbName { get; private set; }
    

    public Dbms(IAppConfiguration config)
    {
        _config = config;
        IsConnected = false;
        ConnectionString = _config.DbConnStr;
        DbName = _config.DbName;
        ConnectToDb();
    }

    public void ConnectToDb()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            Console.WriteLine("Database connection string is not provided.");
            IsConnected = false;
            return;
        }
        try
        {
            var client = new MongoClient(ConnectionString);
            Database = client.GetDatabase(_config.DbName);
            Console.WriteLine($"Successfully connected to the database: {_config.DbName}");
            IsConnected = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to the database: {ex.Message}");
            IsConnected = false;
        }
    }


    public  IAppConfiguration GetConfig()
    {
        return _config;
    }
}

}