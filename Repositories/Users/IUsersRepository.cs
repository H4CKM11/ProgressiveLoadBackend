
namespace ProgressiveLoadBackend.Repositories.Users
{
    public interface IUsersRepository
    {
        Task addUser(Models.Users user);
        Task addSession(Models.Sessions session);
        Task<Models.Users?> getUserByEmail(string email);
  

    }
}
