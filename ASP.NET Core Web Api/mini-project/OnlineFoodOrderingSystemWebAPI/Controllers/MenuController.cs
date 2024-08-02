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

        /// <summary>
        /// Creates a new menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns>This endpoint returns a newly created menu</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/menu
        ///     {
        ///         "id": 1,
        ///         "name": "Gurame Goreng",
        ///         "price": 20000,
        ///         "category": "Food",
        ///         "rating": 4
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">Returns the newly created menu</response>
        /// <response code="400">If menu already exist</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Get all menu data
        /// </summary>
        /// <returns>This endpoint returns a list of menu</returns>
        /// <response code="200">Returns a list of menu</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllMenu()
        {
            return Ok(_menuService.GetAllMenu());
        }

        /// <summary>
        /// Get a specific menu data
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This endpoint returns a menu data</returns>
        /// <response code="200">Returns a menu</response>
        /// <response code="400">If id is less than or equal to 0</response>
        /// <response code="404">If the menu does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Updates an existing menu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputMenu"></param>
        /// <returns>This endpoint returns a newly updated menu</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/menu/{id}
        ///     {
        ///         "id": 1,
        ///         "name": "Ayam Goreng",
        ///         "price": 15000,
        ///         "category": "Food",
        ///         "rating": 4.5
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns the newly updated menu</response>
        /// <response code="400">If id is less than or equal to 0</response>
        /// <response code="404">If menu does not exist</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a specific menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <response code="204">Successful menu delete</response>
        /// <response code="400">If id is less than or equal to 0</response>
        /// <response code="404">If menu does not exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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


        /// <summary>
        /// Add a new rating to a menu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rating"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request
        /// 
        ///     POST api/menu/{id}
        ///     rating=4
        ///     
        /// </remarks>
        /// <response code="204">Successful add rating</response>
        /// <response code="400">If id is less than or equal to 0</response>
        /// <response code="404">If menu does not exist</response>
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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