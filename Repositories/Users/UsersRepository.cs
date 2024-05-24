using ProgressiveLoadBackend.Data;
using ProgressiveLoadBackend.DTOs;
using ProgressiveLoadBackend.Models;

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

            try {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " Error adding User");
            }

        }

        public async Task<Guid> generateSessionID(Models.Users user)
        {
            try {
                Models.Sessions session = new Models.Sessions
                {
                    sessionID = Guid.NewGuid(),
                    userID = user.userID,
                    createdAt = DateTime.Now,
                    expiresAt = DateTime.Now.AddDays(30),
                    user = user
                };

                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();

                return session.sessionID;

            }
            catch (Exception e)
            {
                Console.WriteLine(e + " Error Generating Session");
                throw;
            }
        }

    }
}
