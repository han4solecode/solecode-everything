using HRIS.Application.Contracts;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpDependentController : ControllerBase
    {
        private readonly IEmpDependentService _empDependentService;

        public EmpDependentController(IEmpDependentService empDependentService)
        {
            _empDependentService = empDependentService;
        }

        [Authorize(Roles = "Administrator, HR Manager, Employee")]
        [HttpPost]
        public async Task<IActionResult> CreateDependent([FromBody] EmpDependent empDependent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _empDependentService.AddNewEmpDependent(empDependent);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, HR Manager, Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDependent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var res = await _empDependentService.DeleteEmpDependent(id);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Administrator, HR Manager, Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAllDependents()
        {
            var dependents = await _empDependentService.GetAllEmpDependents();

            return Ok(dependents);
        }

        [Authorize(Roles = "Administrator, HR Manager, Employee")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDependentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dependent = await _empDependentService.GetEmpDependentById(id);

            return Ok(dependent);
        }

        [Authorize(Roles = "Administrator, HR Manager, Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDependent(int id, [FromBody] EmpDependent inputEmpDependent)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var res = await _empDependentService.UpdateExistingEmpDependent(id, inputEmpDependent);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);            
        }
    }
}