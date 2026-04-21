using WebAPI.Domain.Entities;



namespace WebAPI.Domain.Interfaces
{
    public interface ICollectDataFromUV
    {
       void Run();

      bool IsConnected { get; }

        bool IsFinished { get;  }
        Error Error { get; }
        bool ValidSub { get; }

    }
}