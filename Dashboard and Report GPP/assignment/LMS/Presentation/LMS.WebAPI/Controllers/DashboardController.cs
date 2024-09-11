using LMS.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly ILendingService _lendingService;
        private readonly IWorkflowService _workflowService;

        public DashboardController(IBookService bookService, IUserService userService, ILendingService lendingService, IWorkflowService workflowService)
        {
            _bookService = bookService;
            _userService = userService;
            _lendingService = lendingService;
            _workflowService = workflowService;
        }

        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet("kpi")]
        public async Task<IActionResult> Kpi()
        {
            var bookCount = await _bookService.GetTotalNumberOfBooks();
            var overdue = await _lendingService.GetOverdueBooks();
            var members = await _userService.Get10MostActiveMembers();
            var booksPerCategoryCount = await _bookService.GetNumberOfBooksPerCategory();
            var processCount = await _workflowService.GetProcessToReviewCount();

            var overdueBooks = overdue.Select(b => new {
                LendingId = b.Id,
                Name = $"{b.AppUser!.FirstName} {b.AppUser.LastName}",
                Title = b.Book!.Title,
                DueDate = b.DueReturnDate,
                OverdueDays = DateOnly.FromDateTime(DateTime.Now).DayNumber - b.DueReturnDate.DayNumber
            }).ToList();
            var activeMembers = members.Select(m => new {
                Name = $"{m.FirstName} {m.LastName}",
                BorrowedBookCount = m.Lendings.Count
            }).ToList();

            var data = new {
                TotalBooks = bookCount,
                ActiveMembers = activeMembers,
                OverdueBooks = overdueBooks,
                BooksPerCategoryCount = booksPerCategoryCount,
                ProcessToReviewCount = processCount
            };

            return Ok(data);
        }

        [Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet("processes")]
        public async Task<IActionResult> Processes()
        {
            var processToReview = await _workflowService.GetProcessToReview();

            return Ok(processToReview);
        }
        
    }

}