using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystemAPI.Interfaces;
using StudentSystemAPI.Models;

namespace StudentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuruController : ControllerBase
    {
        private readonly IGuruService _guruService;

        public GuruController(IGuruService guruService)
        {
            _guruService = guruService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var guru = _guruService.GetAllGuru();
            return Ok(guru);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Guru guru)
        {
            _guruService.AddGuru(guru);
            return Created($"/guru/{guru.GuruId}", guru);
        }
    }
}
