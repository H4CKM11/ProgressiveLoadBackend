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

                //Adding new user into database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " Error adding User");
            }

        }

        public async Task addSession(Models.Sessions session)
        {
            try {

                //Adding new Session into Database
                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " Error Generating Session");
                throw;
            }
        }

        public async Task login(LoginDTO login)
        {
            try {
                
            } catch (Exception e)
            {
                Console.WriteLine(e + " Error Logining in");
                throw;

            }
        }
    }
}
