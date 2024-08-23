using HRIS.Application.Contracts;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var locations = await _locationService.GetAllLocations(recordsPerPage, currentPage);

            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await _locationService.GetLocationById(id);

            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation([FromBody] Location location)
        {
            var isCreated = await _locationService.AddNewLocation(location);

            if (!isCreated)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetLocationById), new { id = location.Locationid}, location);
        }
    }
}