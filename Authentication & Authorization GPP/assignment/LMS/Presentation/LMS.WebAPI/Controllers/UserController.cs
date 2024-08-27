using LMS.Application.Contracts;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var users = await _userService.GetAllUsers(recordsPerPage, currentPage);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var isCreated = await _userService.AddNewUser(user);

            if (!isCreated)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id}, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User inputUser)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isUpdated = await _userService.UpdateExistingUser(id, inputUser);

                if (!isUpdated)
                {
                    return BadRequest();
                }

                return Ok(inputUser);
                // return 
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isDeleted = await _userService.DeleteExistingUser(id);

                if (!isDeleted)
                {
                    return BadRequest();
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