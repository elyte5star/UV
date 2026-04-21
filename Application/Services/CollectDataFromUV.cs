using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Entities;
using Microsoft.Extensions.Logging;
namespace WebAPI.Application.Services
{
    public class CollectDataFromUV : ICollectDataFromUV
    {

        private readonly IMQTTBroker _mqttBroker;

        private readonly ILogger<CollectDataFromUV> _logger;

        public string UserId { get; private set; }

        public bool IsConnected { get; private set; }

        public bool IsFinished { get; private set; }

        public Error Error { get; private set; }

        private readonly IAuthClient _authClient;

        private readonly ISubscription _subscription;

        public IAppTimer _timer;

        public bool ValidSub { get; private set; } = true;

        public IUVRepository _uvRepository;

        public CollectDataFromUV(IAppTimer timer, IUVRepository uvRepository, IMQTTBroker mqttBroker, ISubscription subscription, IAuthClient authClient, ILogger<CollectDataFromUV> logger)
        {
            _timer = timer;
            _uvRepository = uvRepository;
            _mqttBroker = mqttBroker;
            _subscription = subscription;
            _authClient = authClient;
            _logger = logger;
            

        }
        
        public void Run()
        {

            UserId = _authClient.UserId;

            ValidSub = _subscription.CheckValidSubscription(UserId);

            if (!ValidSub)
            {
                Error = new Error("Invalid subscription for user.", DateTime.Now, 0, ErrorCategory.SubscriptionError);
                _logger.LogError("Invalid subscription for user {UserId}", UserId);
                return;
            }

            IsConnected = _mqttBroker.ConnectToBroker();
            if (!IsConnected)
            {
                Error = new Error("Failed to connect to MQTT broker.", DateTime.Now, 0, ErrorCategory.BrokerError);
                _logger.LogError("Failed to connect to MQTT broker");
                return;
            }
                _mqttBroker.SubscribeToTopics();
                IsFinished = _mqttBroker.CheckFinishedSignal();
        }

    }
}