namespace ProgressiveLoadBackend.Repositories.Users
{
    public interface IUsersRepository
    {
        Task addUser(Models.Users user);
        Task<Guid> generateSessionID(Models.Users user);
    }
}
