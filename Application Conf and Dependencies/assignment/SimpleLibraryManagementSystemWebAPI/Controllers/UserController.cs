using Microsoft.AspNetCore.Mvc;
using SimpleLibraryManagementSystemWebAPI.Interfaces;
using SimpleLibraryManagementSystemWebAPI.Models;
using SimpleLibraryManagementSystemWebAPI.Services;

namespace SimpleLibraryManagementSystemWebAPI.Controllers
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

        [HttpPost("lend")]
        public async Task<IActionResult> BorrowBook([FromBody] LendingDto lending)
        {
            var lendTransac = await _userRepository.BorrowBook(lending.UserId, lending.Books);

            if (lendTransac == null)
            {
                return BadRequest();
            }

            return Created("Success!", lendTransac);

            // BookManager bookManager = BookManager.Instance;

            // bookManager.BorrowBook();
        }
    }
}