using Microsoft.AspNetCore.Mvc;
using ProgressiveLoadBackend.DTOs;
using ProgressiveLoadBackend.Models;
using ProgressiveLoadBackend.Repositories.Users;

namespace ProgressiveLoadBackend.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;

        UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerDTO)
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
                //Change this to passwordHash
                passwordHash = registerDTO.Password
            };

            _usersRepository.addUser(user);
            _usersRepository.generateSessionID(user);

            return Ok(registerDTO);
        }
    }
}
