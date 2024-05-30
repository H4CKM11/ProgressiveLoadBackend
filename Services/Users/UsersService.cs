using ProgressiveLoadBackend.DTOs;
using ProgressiveLoadBackend.Models;
using ProgressiveLoadBackend.Repositories.Users;
using ProgressiveLoadBackend.Services.HashingService;


//Higher Level Of User Repository
namespace ProgressiveLoadBackend.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHashingService _hashingService;

        public UsersService(IUsersRepository usersRepository, IHashingService hashingService)
        {
            _usersRepository = usersRepository;
            _hashingService = hashingService;
        }

        public async Task<Models.Users> addUserToRepository(RegisterDTO registerDTO)
        {
            Models.Users user = new Models.Users
            {
                userID = Guid.NewGuid(),
                firstName = registerDTO.firstName,
                lastName = registerDTO.lastName,
                email = registerDTO.Email,
                passwordHash = _hashingService.hashPassword(registerDTO.Password)
            };

            try
            { 
                await _usersRepository.addUser(user);
                return user;
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

                await _usersRepository.addSession(session);

                return session;

            }
            catch (Exception e)
            {
                Console.WriteLine(e + " Error Generating Session");
                throw;
            }
        }

        public async Task<loginResult> login(LoginDTO userLogin)
        {
            try
            {
                var user = await _usersRepository.getUserByEmail(userLogin.Email);
                if (user == null)
                {
                    return new loginResult { success = false, message = "User Not Found" };
                } 


                bool passwordValid = _hashingService.verifyPassword(user.passwordHash, userLogin.Password);
                if (passwordValid == false) {
                    return new loginResult { success = false, message = "Incorrect Username or Password" };
                }


                return new loginResult { success = true, message = "Success", user = user };

            } catch (Exception ex)
            {
                Console.WriteLine(ex + " Error Loggin In");
                throw;
            }
        }

        public async Task<verificationResult> verifySessionID(string sessionID)
        {
            var session = await _usersRepository.getSession(sessionID);

            if(session == null)
            {
                return new verificationResult { success = false, message = "Session Not Found" };
            }


            if (session.expiresAt < DateTime.Now)
            {
                await _usersRepository.removeSession(session);
                return new verificationResult { success = false, message = "Session Expired" };
            }
            else
            {
                var user = await _usersRepository.getUserByUserID(session.userID);
                if (user == null)
                {
                   return new verificationResult { success = false, message = "User Not Found" };

                }

                return new verificationResult { success = true, message = "Session Valid",
                                                firstName = user.firstName, lastName = user.lastName };
            }

        }
    }

    public class loginResult
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public Models.Users? user { get; set; }
    }

    public class verificationResult
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
    }
}
