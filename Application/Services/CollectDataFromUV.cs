using WebAPI.Data.Config;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Entities;
namespace WebAPI.Application.Services
{
    public class CollectDataFromUV : ICollectDataFromUV
    {

        private readonly IMQTTBroker _mqttBroker;

        public string userId;

        public bool IsConnected { get; private set; }

        public bool IsFinished { get; private set; }

        public Error Error { get; private set; }

        private readonly IAuthClient _authClient;

        private readonly ISubscription _subscription;

        public IAppTimer _timer;

        public bool validSub = true;


        public IUVRepository _uvRepository;

        public CollectDataFromUV(IAppTimer timer, IUVRepository uvRepository, IMQTTBroker mqttBroker, ISubscription subscription, IAuthClient authClient)
        {
            _timer = timer;
            _uvRepository = uvRepository;
            _mqttBroker = mqttBroker;
            _subscription = subscription;
            _authClient = authClient;


        }

        public void Run()
        {
            IsConnected = _mqttBroker.ConnectToBroker();

            userId = _authClient.UserId;
            
            validSub = _subscription.CheckValidSubscription(userId);

            if (!IsConnected)
            {
                Error = new Error("Failed to connect to MQTT broker.", DateTime.Now, 0, ErrorCategory.BrokerError);
                return;
            }
            else if (!validSub)
            {
                Error = new Error("Invalid subscription for user.", DateTime.Now, 0, ErrorCategory.SubscriptionError);
                return;
            }
            else
            {
                _mqttBroker.SubscribeToTopics();
                IsFinished = _mqttBroker.CheckFinishedSignal();
            }
        }


    }
}