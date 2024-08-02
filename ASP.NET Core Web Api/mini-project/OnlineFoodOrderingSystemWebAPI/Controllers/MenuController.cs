using Microsoft.AspNetCore.Mvc;
using OnlineFoodOrderingSystemWebAPI.Interfaces;
using OnlineFoodOrderingSystemWebAPI.Models;

namespace OnlineFoodOrderingSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        public IActionResult AddMenu(Menu menu)
        {
            var existingMenu = _menuService.GetMenuById(menu.Id);

            if (existingMenu != null)
            {
                return BadRequest("Menu with this id already exist");
            }

            _menuService.AddMenu(menu);
            return Created($"menu/{menu.Id}", menu);
        }

        [HttpGet]
        public IActionResult GetAllMenu()
        {
            return Ok(_menuService.GetAllMenu());
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuDetail(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id input");
            }

            var menu = _menuService.GetMenuById(id);

            if (menu == null)
            {
                return NotFound("Menu does not exist");
            }

            return Ok(menu);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMenu(int id, [FromBody] Menu inputMenu)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id input");
            }

            var updatedMenu = _menuService.UpdateMenu(id, inputMenu);

            if (updatedMenu == null)
            {
                return NotFound("Menu does not exist");
            }

            return Ok(updatedMenu);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id input");
            }

            var isDeleted = _menuService.DeleteMenu(id);

            if (!isDeleted)
            {
                return NotFound("Menu does not exist");
            }

            return NoContent();
        }

        [HttpPost("{id}")]
        public IActionResult AddRating(int id, [FromForm] double rating)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id input");
            }

            if (rating < 0 || rating > 5)
            {
                return BadRequest("Rating must be between 1 to 5");
            }

            var isRatingAdded = _menuService.AddRating(id, rating);

            if (!isRatingAdded)
            {
                return NotFound("Menu does not exist");
            }

            return NoContent();
        }
    }
}