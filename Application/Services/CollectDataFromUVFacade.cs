
using WebAPI.Domain.Interfaces;

using WebAPI.Data.Repositories;

namespace WebAPI.Application.Services
{
    public class CollectDataFromUVFacade
    {
        private readonly ICollectDataFromUV _collectDataFromUV;
        private readonly IAppTimer _timer;

        private readonly IUVRepository _uvRepository;

        public CollectDataFromUVFacade(IAppTimer timer, IUVRepository uvRepository, ICollectDataFromUV collectDataFromUV)
        {
             _timer = timer;
            _uvRepository = uvRepository;
            _collectDataFromUV = collectDataFromUV;
            _collectDataFromUV.Run();
            
        }
       
    }
       
}
