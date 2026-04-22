
namespace WebAPI.Domain.Interfaces
{
    public interface IAuthClient
    {
        bool ValidSubscription(string userId);

        public string UserId { get; }
        public string UserName { get; }

        public bool IsActive { get; }

        public string SessionToken { get; }

        public DateTime LastLogin { get; }

        public string[] Roles { get; }

        public string Email { get; }

    }
}