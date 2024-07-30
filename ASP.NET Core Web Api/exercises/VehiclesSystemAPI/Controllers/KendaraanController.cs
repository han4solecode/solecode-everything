using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehiclesSystemAPI.Interfaces;

namespace VehiclesSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KendaraanController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllKendaraan()
        {

        }
    }
}
