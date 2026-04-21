using WebAPI.Domain.Entities;
using MQTTnet;

namespace WebAPI.Domain.Interfaces
{
    public interface IMQTTBroker
    {
        void Connect();
        void PublishMessage(string topic, string payload);
        void Disconnect();

        void SubscribeToTopics();

        MQTTData UnPackData(string payload, string topic, string clientId);


        void HandleReceivedMessage(MqttApplicationMessageReceivedEventArgs e);

        void SaveMessageToDb(MQTTData data);

        bool ConnectToBroker();

        bool CheckFinishedSignal();
    }
}