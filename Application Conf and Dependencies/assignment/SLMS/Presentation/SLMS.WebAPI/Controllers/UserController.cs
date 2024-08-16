using Microsoft.AspNetCore.Mvc;
using SLMS.Application.Repositories;
using SLMS.Domain.Entities;

namespace SLMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userRepository.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userRepository.GetUserById(userId);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                await _userRepository.AddUser(user);

                return Created($"api/user/{user.Userid}", user);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(int userId, [FromBody] User inputUser)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var userUpdated = await _userRepository.UpdateUser(userId, inputUser);

                if (userUpdated == null)
                {
                    return NotFound();
                }

                return Ok(userUpdated);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isDeleted = await _userRepository.DeleteUser(userId);

                if (!isDeleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}