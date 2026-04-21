
using WebAPI.Domain.Entities;
using MQTTnet;
using MQTTnet.Protocol;
using System.Text;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Services
{
    public class MQTTBroker : IMQTTBroker
    {
        private readonly IAppConfiguration _config;

        public bool IsConnected = false;

        public bool IsFinished { get; private set; } = false;

        private readonly IUVRepository _uvRepository;

        public IMqttClient MqttClient { get; private set; }


        private readonly IAppTimer _timer;

        public MQTTBroker(IAppTimer timer, IUVRepository uvRepository, IAppConfiguration config)
        {
            _uvRepository = uvRepository;
            _timer = timer;
            _config = config;
        }


        public bool ConnectToBroker()
        {
            Connect();
            return IsConnected;
        }
        public void Connect()
        {
            var factory = new MqttClientFactory();

            MqttClient = factory.CreateMqttClient();

            var mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(_config.BrokerAddress, _config.BrokerPort)
            .WithClientId(_config.BrokerClientId)
            .WithCleanSession()
            .Build();

            try
            {
                MqttClient.ConnectAsync(mqttOptions, CancellationToken.None).Wait();
                Console.WriteLine("Connected to MQTT broker successfully.");
                IsConnected = true;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to MQTT broker: {ex.Message}");
                IsConnected = false;
            }


        }

        public void SubscribeToTopics()
        {
            if (!IsConnected || _config.BrokerTopics.Length == 0)
            {
                Console.WriteLine("Cannot subscribe to topics because MQTT client is not connected.");
                return;
            }

            foreach (var topic in _config.BrokerTopics)
            {
                MqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce).Build()).Wait();
                Console.WriteLine($"Subscribed to topic: {topic}");
            }
        }

        public void Disconnect()
        {
            if (MqttClient != null && MqttClient.IsConnected)
            {
                MqttClient.DisconnectAsync().Wait();
                Console.WriteLine("Disconnected from MQTT broker.");
                IsConnected = false;
            }
        }

        public void PublishMessage(string topic, string payload)
        {
            if (!IsConnected)
            {
                Console.WriteLine("Cannot publish message because MQTT client is not connected.");
                return;
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            MqttClient.PublishAsync(message).Wait();
            Console.WriteLine($"Published message to topic '{topic}': {payload}");
        }

        public MQTTData UnPackData(string payload, string topic, string clientId)
        {
            var data = new MQTTData
            {
                Payload = payload,
                Topic = topic,
                ClientId = clientId,
                MessageDate = DateTime.UtcNow
            };

            return data;
        }

        public void HandleReceivedMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            var clientId = e.ClientId;
            Console.WriteLine($"Received message on topic '{topic}': {payload}");
            MQTTData sensorData = UnPackData(payload, topic, clientId);
            SaveMessageToDb(sensorData);
            _timer.Wait(_config.WaitTime); // Wait for the specified time before allowing the next message to be processed
        }
        public void SaveMessageToDb(MQTTData data)
        {
            _timer.Start();
            if (data != null)
            {
                _uvRepository.SaveUVData(data).Wait();
                Console.WriteLine("MQTT data saved to database.");
            }
            else
            {
                Console.WriteLine("No MQTT data to save to database.");
            }
        }
        public bool CheckFinishedSignal()

        {
            _timer.EndTime = DateTime.Now;
            if (_timer.GetElapsedTime() >= TimeSpan.FromMinutes(20).Ticks)
            {
                IsFinished = true;
            }
            return IsFinished;
        }
    }




}