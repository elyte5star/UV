namespace WebAPI.Domain.Interfaces
{
    public interface IAppConfiguration
    {
        string BrokerAddress { get; }
        int BrokerPort { get; }
        string BrokerUsername { get; }
        string BrokerPassword { get; }
        string BrokerClientId { get; }
        string[] BrokerTopics { get; }

        string DbConnStr { get; }

        string DbName { get; }

        int WaitTime { get; }
    }




}