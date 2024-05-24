using Microsoft.AspNetCore.Mvc;
using ProgressiveLoadBackend.DTOs;
using ProgressiveLoadBackend.Models;
using ProgressiveLoadBackend.Repositories.Users;
using ProgressiveLoadBackend.Services.HashingService;

namespace ProgressiveLoadBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHashingService _hashingService;

        public UsersController(IUsersRepository usersRepository, IHashingService hashingService)
        {
            _usersRepository = usersRepository;
            _hashingService = hashingService;
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

            await _usersRepository.addUser(user);

            Guid sessionID = await _usersRepository.generateSessionID(user);

            return Ok(sessionID);
        }
    }
}
