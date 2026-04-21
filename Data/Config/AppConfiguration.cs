using WebAPI.Domain.Interfaces;

namespace WebAPI.Data.Config
{

    public sealed class AppConfiguration : IAppConfiguration
    {
        public string BrokerAddress { get; set; }
        public int BrokerPort { get; set; }
        public string BrokerUsername { get; set; }
        public string BrokerPassword { get; set; }
        public string BrokerClientId { get; set; }
        public string[] BrokerTopics { get; set; }

        public string DbConnStr { get; set; }

        public string DbName { get; set; }

        public int WaitTime { get; set; }

        

    }
}