namespace WebAPI.Domain.Interfaces
{
    public interface IAppTimer
    {
        void Start();
        void Stop();
        long GetElapsedTime();
        void Wait(int duration);

        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
    }
}