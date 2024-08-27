using LMS.Application.Contracts;
using LMS.Application.DTOs.Login;
using LMS.Application.DTOs.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.SignUpAsync(model);

            if (result.Status == "Error")
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(model);

            if (result.Status == "Error")
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("role")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] string roleName)
        {
            var result = await _authService.CreateRoleAsync(roleName);

            return Ok(result);
        }

        [Authorize(Roles = "Librarian")]
        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterLibraryUserAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterLibraryUser(model);

            if (result.Status == "Error")
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Library Manager")]
        [HttpPost("register-librarian")]
        public async Task<IActionResult> RegisterLibrarianAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterLibrarian(model);

            if (result.Status == "Error")
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("register-library-manager")]
        public async Task<IActionResult> RegisterLibraryManagerAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterLibraryManager(model);

            if (result.Status == "Error")
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}