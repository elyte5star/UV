
using WebAPI.Domain.Interfaces;

namespace WebAPI.Domain.Entities
{
    public class Subscription : ISubscription
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Status { get; set; } = "Inactive";

        public bool RenewedSub { get; set; } = false;

        public DateTime EndOfSub { get; set; } = DateTime.MinValue;

        public DateTime DateOfSub { get; set; } = DateTime.MinValue;

        public string[] Services { get; set; }

        public bool CheckValidSubscription(string userId)
        {
            return true;
        }
        
    }
}