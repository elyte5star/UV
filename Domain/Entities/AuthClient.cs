

using WebAPI.Domain.Interfaces;
namespace WebAPI.Domain.Entities
{
   
    
    public class AuthClient : IAuthClient
    {
        public string UserId { get; set; } = "3a123c17-0093-49e3-bb3a-5bd28cb67496";
        public string UserName { get; set; }

        public bool IsActive { get; set; } = false;

        public string SessionToken { get; set; }

        public DateTime LastLogin { get; set; } 

        public string[] Roles { get; set; } 

        public string Email { get; set; }

        public bool ValidSubscription(string userId)
        {
            return true;
        }


    }
}