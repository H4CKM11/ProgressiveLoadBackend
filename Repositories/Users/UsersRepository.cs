
using ProgressiveLoadBackend.Data;

namespace ProgressiveLoadBackend.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        public readonly dbContext _context;
        public UsersRepository(dbContext context)
        {
            _context = context;
        }


        public async Task addUser(Models.Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task generateSessionID(Models.Users user)
        {
            throw new NotImplementedException();
        }
    }
}
