using Microsoft.EntityFrameworkCore;
using ProgressiveLoadBackend.Data;


//Lower Level Of User Repository
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

        public async Task addSession(Models.Sessions session)
        {
                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();   
        }
        public async Task removeSession(Models.Sessions session)
        {
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }

        public Task<Models.Users?> getUserByEmail(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.email == email);
        }

        public Task<Models.Users?> getUserByUserID(Guid userID)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.userID == userID);
        }

        public Task<Models.Sessions?> getSession(string sessionID)
        {            
            return _context.Sessions.FirstOrDefaultAsync(s => s.sessionID.ToString() == sessionID);
        }
    }
}
