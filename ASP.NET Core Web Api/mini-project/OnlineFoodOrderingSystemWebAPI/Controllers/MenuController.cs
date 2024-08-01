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
                return BadRequest();
            }

            var menu = _menuService.GetMenuById(id);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMenu(int id, [FromBody] Menu inputMenu)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var updatedMenu = _menuService.UpdateMenu(id, inputMenu);

            if (updatedMenu == null)
            {
                return NotFound();
            }

            return Ok(updatedMenu);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isDeleted = _menuService.DeleteMenu(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // [HttpPost("{id}")]
        // public IActionResult AddRating(int id, [FromForm] int rating)
        // {
            
        // }
    }
}