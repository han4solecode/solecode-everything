using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehiclesSystemAPI.Interfaces;
using VehiclesSystemAPI.Models;
using VehiclesSystemAPI.Services;

namespace VehiclesSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KendaraanController : ControllerBase
    {
        private readonly IKendaraanService _kendaraanService;

        public KendaraanController(IKendaraanService kendaraanService)
        {
            _kendaraanService = kendaraanService;
        }

        [HttpGet]
        public IActionResult GetAllKendaraan()
        {
            return Ok(_kendaraanService.GetAllKendaraan());
        }

        [HttpGet("{id}")]
        public IActionResult GetKendaranById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var kendaraan  = _kendaraanService.GetKendaraanById(id);

            if (kendaraan == null)
            {
                return NotFound();
            }

            return Ok(kendaraan);
        }

        [HttpPost]
        [Route("mobil")]
        public IActionResult AddMobil([FromBody] Mobil mobil)
        {
            _kendaraanService.AddKendaraan(mobil);
            return Created($"kendaraan/{mobil.Id}", mobil);
        }

        [HttpPost]
        [Route("motor")]
        public IActionResult AddMotor([FromBody] Motor motor)
        {
            _kendaraanService.AddKendaraan(motor);
            return Created($"kendaraan/{motor.Id}", motor);
        }

        [HttpPost]
        [Route("mobillistrik")]
        public IActionResult AddMobilListrik([FromBody] MobilListrik mobilListrik)
        {
            _kendaraanService.AddKendaraan(mobilListrik);
            return Created($"kendaraan/{mobilListrik.Id}", mobilListrik);
        }

        [HttpPost]
        [Route("motorlistrik")]
        public IActionResult AddMotorListrik([FromBody] MotorListrik motorListrik)
        {
            _kendaraanService.AddKendaraan(motorListrik);
            return Created($"kendaraan/{motorListrik.Id}", motorListrik);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateKendaraan(int id, [FromBody] Kendaraan inputKendaraan)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var kendaraan = _kendaraanService.UpdateKendaraan(id, inputKendaraan);

            if (kendaraan == null)
            {
                return NotFound();
            }

            return Ok(kendaraan);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteKendaran(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isDeleted = _kendaraanService.DeleteKendaraan(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("charge")]
        public IActionResult ChargeKendaraanListrik([FromQuery] int jumlah)
        {
            _kendaraanService.ChargeKendaraanListrik(jumlah);
            return NoContent();
        }

        [HttpGet]
        [Route("info")]
        public IActionResult TamplikanSemuaKendaraan()
        {
            var infoKendaraan = _kendaraanService.TampilkanSemuaKendaraan();

            if (infoKendaraan == null)
            {
                return NotFound();
            }

            return Ok(infoKendaraan);
        }

    }
}
