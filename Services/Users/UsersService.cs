using ProgressiveLoadBackend.Models;
using ProgressiveLoadBackend.Repositories.Users;

namespace ProgressiveLoadBackend.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task addUserToRepository(Models.Users user)
        {
            try { 
                await _usersRepository.addUser(user);
            } catch (Exception ex)
            {
                Console.WriteLine(ex + " Adding User Failed");
                throw;
            }
        }

        public async Task<Sessions> generateSession(Models.Users user)
        {
            try
            {
                Models.Sessions session = new Models.Sessions
                {
                    sessionID = Guid.NewGuid(),
                    userID = user.userID,
                    createdAt = DateTime.Now,
                    expiresAt = DateTime.Now.AddDays(30),
                    user = user
                };

                //Adding new Session into Database
                await _usersRepository.addSession(session);

                return session;

            }
            catch (Exception e)
            {
                Console.WriteLine(e + " Error Generating Session");
                throw;
            }
        }
    }
}
