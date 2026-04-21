
using WebAPI.Domain.Interfaces;



namespace WebAPI.Application.Services
{
    public class CollectDataFromUVFacade : BackgroundService
    {
        private readonly ICollectDataFromUV _collectDataFromUV;
        private readonly IAppTimer _timer;

        public CollectDataFromUVFacade(IAppTimer timer, ICollectDataFromUV collectDataFromUV)
        {
             _timer = timer;
            _collectDataFromUV = collectDataFromUV;

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _collectDataFromUV.Run();

            return Task.CompletedTask;
        }

    }
       
}
