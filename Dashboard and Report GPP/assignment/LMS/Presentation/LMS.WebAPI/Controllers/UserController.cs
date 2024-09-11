// using LMS.Application.Contracts;
// using LMS.Domain.Entities;
// using Microsoft.AspNetCore.Mvc;

// namespace LMS.WebAPI.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class UserController : ControllerBase
//     {
//         private readonly IUserService _userService;

//         public UserController(IUserService userService)
//         {
//             _userService = userService;
//         }

//         [HttpGet]
//         public async Task<IActionResult> GetAllUsers([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
//         {
//             var users = await _userService.GetAllUsers(recordsPerPage, currentPage);

//             return Ok(users);
//         }

//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetUserById(int id)
//         {
//             if (id <= 0)
//             {
//                 return BadRequest();
//             }

//             var user = await _userService.GetUserById(id);

//             if (user == null)
//             {
//                 return NotFound();
//             }

//             return Ok(user);
//         }

//         [HttpPost]
//         public async Task<IActionResult> AddUser([FromBody] User user)
//         {
//             var isCreated = await _userService.AddNewUser(user);

//             if (!isCreated)
//             {
//                 return BadRequest();
//             }

//             return CreatedAtAction(nameof(GetUserById), new { id = user.Id}, user);
//         }

//         [HttpPut("{id}")]
//         public async Task<IActionResult> UpdateUser(int id, [FromBody] User inputUser)
//         {
//             if (id <= 0)
//             {
//                 return BadRequest();
//             }

//             try
//             {
//                 var isUpdated = await _userService.UpdateExistingUser(id, inputUser);

//                 if (!isUpdated)
//                 {
//                     return BadRequest();
//                 }

//                 return Ok(inputUser);
//                 // return 
//             }
//             catch (System.Exception)
//             {
//                 return BadRequest();
//             }
//         }

//         [HttpDelete("{id}")]
//         public async Task<IActionResult> RemoveUser(int id)
//         {
//             if (id <= 0)
//             {
//                 return BadRequest();
//             }

//             try
//             {
//                 var isDeleted = await _userService.DeleteExistingUser(id);

//                 if (!isDeleted)
//                 {
//                     return BadRequest();
//                 }

//                 return NoContent();
//             }
//             catch (System.Exception)
//             {
//                 return BadRequest();
//             }
//         }
//     }
// }

using Castle.Components.DictionaryAdapter.Xml;
using LMS.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet("patron-info")]
        public async Task<IActionResult> GetPatronInfoById([FromBody] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _userService.GetPatronInfoById(id);

            return Ok(res);
        }

        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet("patron-info-report")]
        public async Task<IActionResult> A([FromBody] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fileName = "PatronInfo.pdf";
            var file = await _userService.GeneratePatronInfoByIdReport(id);

            if (file == null)
            {
                return BadRequest("Can't generate report");
            }

            return File(file, "application/pdf", fileName);
        }

    }
}