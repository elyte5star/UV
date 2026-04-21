
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Services

{
    public class AppTimer : IAppTimer
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public void Start()
        {
            StartTime = DateTime.Now;
        }

        public void Stop()
        {
            EndTime = DateTime.Now;
        }

        public long GetElapsedTime()
        {
            return (EndTime - StartTime).Ticks;
        }

        public void Wait(int duration)
        {
            System.Threading.Thread.Sleep(duration);
        }


    }
}