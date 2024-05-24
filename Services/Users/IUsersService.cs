using ProgressiveLoadBackend.Models;

namespace ProgressiveLoadBackend.Services.Users
{
    public interface IUsersService
    {
        public Task addUserToRepository(Models.Users user);
        public Task<Sessions> generateSession(Models.Users user);

    }
}
