using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystemAPI.Interfaces;

namespace StudentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemService _systemService;

        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        public ISystemService Get_systemService()
        {
            return _systemService;
        }

        [HttpGet]
        [Route("cari-siswa")]
        public async Task<IActionResult> CariSiswa([FromQuery] string input)
        {
            var student = await _systemService.CariSiswa(input);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet]
        [Route("daftar-siswa")]
        public async Task<IActionResult> DaftarSiswa()
        {
            return Ok(await _systemService.DaftarSiswa());
        }

        [HttpGet]
        [Route("guru-aktif")]
        public async Task<IActionResult> GuruAktif()
        {
            var guruAktif = await _systemService.GuruAktif();

            if (guruAktif == null)
            {
                return NotFound();
            }

            return Ok(guruAktif);
        }

        [HttpGet]
        [Route("kehadiran-rendah")]
        public async Task<IActionResult> KehadiranRendah()
        {
            var studentKehadiranRendah = await _systemService.KehadiranRendah();

            if (studentKehadiranRendah == null)
            {
                return NotFound();
            }

            return Ok(studentKehadiranRendah);
        }

        [HttpGet]
        [Route("siswa-berprestasi")]
        public async Task<IActionResult> SiswaBerprestasi()
        {
            var siswaBerprestasi = await _systemService.SiswaBerprestasi();

            return Ok(siswaBerprestasi);
        }

        [HttpGet]
        [Route("siswa-perjurusan")]
        public async Task<string> SiswaPerJurusan()
        {
            var text = await _systemService.SiswaPerJurusan();

            return text;
        }

        [HttpGet]
        [Route("statistik")]
        public async Task<string> StatistikSekolah()
        {
            return await _systemService.StatistikSekolah();
        }
    }
}
