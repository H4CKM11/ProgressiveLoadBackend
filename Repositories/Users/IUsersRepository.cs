namespace ProgressiveLoadBackend.Repositories.Users
{
    public interface IUsersRepository
    {
        Task addUser(Models.Users user);
        Task generateSessionID(Models.Users user);
    }
}
