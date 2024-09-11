using LMS.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LendController : ControllerBase
    {
        private readonly ILendingService _lendingService;

        public LendController(ILendingService lendingService)
        {
            _lendingService = lendingService;
        }

        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet("overdue-report")]
        public async Task<IActionResult> OverdueReport()
        {
            var fileName = "OverdueReport.pdf";
            var file = await _lendingService.GenerateUserOverdueBooksAndPenaltyReport();

            if (file == null)
            {
                return BadRequest("Can't generate report");
            }

            return File(file, "application/pdf", fileName);
        }

        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet("overdue")]
        public async Task<IActionResult> Overdue()
        {
            var res = await _lendingService.GetUsersOverdueBooksAndPenalty();

            return Ok(res);
        }
    }
}