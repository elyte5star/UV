
using WebAPI.Domain.Interfaces;

using WebAPI.Data.Repositories;

namespace WebAPI.Application.Services
{
    public class CollectDataFromUVFacade : BackgroundService
    {
        private readonly ICollectDataFromUV _collectDataFromUV;
        private readonly IAppTimer _timer;

        private readonly IUVRepository _uvRepository;

        public CollectDataFromUVFacade(IAppTimer timer, IUVRepository uvRepository, ICollectDataFromUV collectDataFromUV)
        {
             _timer = timer;
            _uvRepository = uvRepository;
            _collectDataFromUV = collectDataFromUV;
            
            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _collectDataFromUV.Run();

            return Task.CompletedTask;
        }

    }
       
}
