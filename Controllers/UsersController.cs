using Microsoft.AspNetCore.Mvc;
using ProgressiveLoadBackend.DTOs;
using ProgressiveLoadBackend.Models;
using ProgressiveLoadBackend.Services.Cookies;
using ProgressiveLoadBackend.Services.HashingService;
using ProgressiveLoadBackend.Services.Users;
using System.Net;

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

            try{
                Models.Users user = await _usersService.addUserToRepository(registerDTO);
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
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try{
                loginResult loginResult = await _usersService.login(loginDTO);
                Models.Users? user = loginResult.user;

                if(user == null)
                {
                    return BadRequest(loginResult.message);
                } else if (loginResult.success == false)
                {
                    return BadRequest(loginResult.message);
                }

                Sessions session = await _usersService.generateSession(user);
                var sessionCookie = _cookieService.createSessionCookie(session);
                Response.Cookies.Append("SessionID", session.sessionID.ToString(), sessionCookie);

                return Ok("Login Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " Error Loggin In");
                return BadRequest(ex);
            }
        }

        [HttpPost("verifySessionID")]
        public async Task<IActionResult> verifySessionID()
        {

            var cookie = Request.Cookies["SessionID"];
            if (cookie == null)
            {
                return BadRequest("Cookie Doesnt Exist");
            }

            try
            {
                verificationResult result = await _usersService.verifySessionID(cookie);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + " Error Verifying Session ID");
                return BadRequest(ex);
            }
        }
    }
}
