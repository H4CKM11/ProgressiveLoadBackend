using ProgressiveLoadBackend.Models;
using ProgressiveLoadBackend.DTOs;

namespace ProgressiveLoadBackend.Services.Users
{
    public interface IUsersService
    {
        public Task<Models.Users> addUserToRepository(RegisterDTO registerDTO);
        public Task<Sessions> generateSession(Models.Users user);
        public Task<loginResult> login(LoginDTO userLogin);
        public Task<verificationResult> verifySessionID(string sessionID);

    }
}
