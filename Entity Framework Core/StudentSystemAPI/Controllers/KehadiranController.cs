using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystemAPI.Interfaces;
using StudentSystemAPI.Models;

namespace StudentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KehadiranController : ControllerBase
    {
        private readonly IKehadiranService _kehadiranService;

        public KehadiranController(IKehadiranService kehadiranService)
        {
            _kehadiranService = kehadiranService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var kehadiran = _kehadiranService.GetAllKehadiran();
            return Ok(kehadiran);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Kehadiran kehadiran)
        {
            _kehadiranService.AddKehadiran(kehadiran);
            return Created($"/guru/{kehadiran.KehadiranId}", kehadiran);
        }
    }
}
