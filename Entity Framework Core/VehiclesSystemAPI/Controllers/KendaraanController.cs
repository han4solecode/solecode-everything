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

        /// <summary>
        /// Get all kendaraan data
        /// </summary>
        /// 
        /// <returns>This endpoint returns a list of kendaraan.</returns>
        [HttpGet]
        public IActionResult GetAllKendaraan()
        {
            return Ok(_kendaraanService.GetAllKendaraan());
        }

        /// <summary>
        /// Get kendaraan by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>This endpoint returns a kendaraan.</returns>
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

        /// <summary>
        /// Create a new kendaraan mobil
        /// </summary>
        /// <param name="mobil"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mobil")]
        public IActionResult AddMobil([FromBody] Mobil mobil)
        {
            _kendaraanService.AddKendaraan(mobil);
            return Created($"kendaraan/{mobil.Id}", mobil);
        }

        /// <summary>
        /// Create a new kendaraan motor
        /// </summary>
        /// <param name="motor"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("motor")]
        public IActionResult AddMotor([FromBody] Motor motor)
        {
            _kendaraanService.AddKendaraan(motor);
            return Created($"kendaraan/{motor.Id}", motor);
        }

        /// <summary>
        /// Create a new kendaraan mobil listrik
        /// </summary>
        /// <param name="mobilListrik"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mobillistrik")]
        public IActionResult AddMobilListrik([FromBody] MobilListrik mobilListrik)
        {
            _kendaraanService.AddKendaraan(mobilListrik);
            return Created($"kendaraan/{mobilListrik.Id}", mobilListrik);
        }

        /// <summary>
        /// Create a new kendaraan motor listrik
        /// </summary>
        /// <param name="motorListrik"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("motorlistrik")]
        public IActionResult AddMotorListrik([FromBody] MotorListrik motorListrik)
        {
            _kendaraanService.AddKendaraan(motorListrik);
            return Created($"kendaraan/{motorListrik.Id}", motorListrik);
        }

        /// <summary>
        /// Update a kendaraan by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputKendaraan"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete a kendaraan by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Charge all kendaraan listrik
        /// </summary>
        /// <param name="jumlah"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("charge")]
        public IActionResult ChargeKendaraanListrik([FromQuery] int jumlah)
        {
            _kendaraanService.ChargeKendaraanListrik(jumlah);
            return NoContent();
        }


        /// <summary>
        /// Display all kendaraan info
        /// </summary>
        /// <returns></returns>
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
