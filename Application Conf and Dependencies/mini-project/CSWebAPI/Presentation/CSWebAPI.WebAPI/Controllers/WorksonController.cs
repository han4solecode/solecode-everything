using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CSWebAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorksonController : ControllerBase
    {
        // private readonly IWorksonRepository _worksonRepository;
        private readonly IWorksonService _worksonService;

        public WorksonController(IWorksonService worksonService)
        {
            // _worksonRepository = worksonRepository;
            _worksonService = worksonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var worksons = await _worksonService.GetAllWorksons(recordsPerPage, currentPage);

            return Ok(worksons);
        }

        [HttpGet("{empNo}/{projNo}")]
        public async Task<IActionResult> GetById(int empNo, int projNo)
        {
            if (empNo <= 0 || projNo <= 0)
            {
                return BadRequest();
            }

            var workson = await _worksonService.GetWorksonById(empNo, projNo);

            return Ok(workson);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Workson workson)
        {
            var isCreated = await _worksonService.AddNewWorkson(workson);

            if (isCreated)
            {
                return CreatedAtAction(nameof(GetById), new { empNo = workson.Empno, projNo = workson.Projno }, workson);
                
            }
            else
            {
                return BadRequest();
            }


            // try
            // {
            //     await _worksonRepository.AddWorkson(workson);

            //     return CreatedAtAction(nameof(GetById), new { empNo = workson.Empno, projNo = workson.Projno }, workson);
            // }
            // catch (System.Exception)
            // {
            //     return BadRequest();
            // }
        }

        [HttpPut("{empNo}/{projNo}")]
        public async Task<IActionResult> UpdateWorkson(int empNo, int projNo, [FromBody] Workson inputWorkson)
        {
            if (empNo <= 0 || projNo <= 0)
            {
                return BadRequest();
            }

            try
            {
                var woUpdated = await _worksonService.UpdateExistingWorkson(empNo, projNo, inputWorkson);

                if (!woUpdated)
                {
                    return NotFound();
                }

                return Ok(inputWorkson);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{empNo}/{projNo}")]
        public async Task<IActionResult> DeleteWorkson(int empNo, int projNo)
        {
            if (empNo <= 0 || projNo <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isWoDeleted = await _worksonService.DeleteWorkson(empNo, projNo);

                if (!isWoDeleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }
    }
}