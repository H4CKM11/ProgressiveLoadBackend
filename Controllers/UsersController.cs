using Microsoft.AspNetCore.Mvc;
using ProgressiveLoadBackend.DTOs;
using ProgressiveLoadBackend.Models;
using ProgressiveLoadBackend.Repositories.Users;
using ProgressiveLoadBackend.Services.Cookies;
using ProgressiveLoadBackend.Services.HashingService;
using ProgressiveLoadBackend.Services.Users;

namespace ProgressiveLoadBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IHashingService _hashingService;
        private readonly ICookieService _cookieService;
        private readonly IUsersService _usersService;

        public UsersController(IHashingService hashingService, ICookieService cookieService, IUsersService usersService)
        {
            _hashingService = hashingService;
            _cookieService = cookieService;
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = new Users
            {
                userID = Guid.NewGuid(),
                firstName = registerDTO.firstName,
                lastName = registerDTO.lastName,
                email = registerDTO.Email,
                passwordHash = _hashingService.hashPassword(registerDTO.Password)
            };

            try{
                await _usersService.addUserToRepository(user);
                Sessions session = await _usersService.generateSession(user);

                var sessionCookie = _cookieService.createSessionCookie(session);
                Response.Cookies.Append("SessionID", session.sessionID.ToString(), sessionCookie);

                return Ok("Registered User Success");
            } catch(Exception ex)
            {
                Console.WriteLine(ex + " Error adding user");
                return BadRequest(ex);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok();
        }
    }
}
