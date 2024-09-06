using LMS.Application.Contracts;
using LMS.Application.DTOs.Book;
using LMS.Application.DTOs.Email;
using LMS.Application.DTOs.Request;
using LMS.Application.Persistance;
using LMS.Application.Persistance.Helper;
using LMS.Domain.Entities;
using LMS.Domain.Entities.Workflow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LMS.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepostory _bookRepostory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IEmailService _emailService;
        private readonly RoleManager<AppRole> _roleManager;

        public BookService(IBookRepostory bookRepostory, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IWorkflowRepository workflowRepository, IEmailService emailService, RoleManager<AppRole> roleManager)
        {
            _bookRepostory = bookRepostory;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _workflowRepository = workflowRepository;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public async Task<bool> AddNewBook(Book book)
        {
            try
            {
                await _bookRepostory.Create(book);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public async Task<bool> DeleteExistingBook(int id)
        {
            var book = await _bookRepostory.GetById(id);

            if (book == null)
            {
                return false;
            }

            try
            {
                await _bookRepostory.Delete(book);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooks(int a, int b)
        {
            var books = await _bookRepostory.GetAll(a, b);

            return books;
        }

        public async Task<Book?> GetBookById(int id)
        {
            var book = await _bookRepostory.GetById(id);

            return book;
        }

        public async Task<bool> UpdateExistingBook(int id, Book inputBook)
        {
            var book = await _bookRepostory.GetById(id);

            if (book == null)
            {
                return false;
            }

            book.Author = inputBook.Author;
            book.Category = inputBook.Category;
            book.Description = inputBook.Description;
            book.ISBN = inputBook.ISBN;
            book.Location = inputBook.Location;
            book.Price = inputBook.Price;
            book.Publisher = inputBook.Publisher;
            book.PurchaseDate = inputBook.PurchaseDate;
            book.Title = inputBook.Title;

            await _bookRepostory.Update(book);

            return true;
        }

        public async Task<IEnumerable<Book>> SearchBookByQuery(QueryObject query, int a, int b)
        {
            var books = await _bookRepostory.SearchBook(query, a, b);

            return books;
        }

        public async Task<bool> BookSoftDelete(int id, string? reason)
        {
            var book = await _bookRepostory.GetById(id);

            if (book == null)
            {
                return false;
            }

            try
            {
                await _bookRepostory.BookSoftDelete(book, reason);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        // used by library user
        public async Task<bool> BookRequest(BookRequestDto bookRequest)
        {
            // get username from httpcontextaccessor
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var user = await _userManager.FindByNameAsync(userName!);

            var workflows = await _workflowRepository.GetAllWorkflows();
            var bookRequestWorkflowId = workflows.Where(w => w.WorkflowName == "Book Request").Select(w => w.WorkflowId).First();

            // get NextStepId
            var userRole = await _roleManager.FindByNameAsync("Library User");
            var userRoleId = userRole!.Id;
            var wsws = await _workflowRepository.GetAllWorkflowSequences();
            var stepId = wsws.Where(ws => ws.WorkflowId == bookRequestWorkflowId && ws.RequiredRoleId == userRoleId).Select(ws => ws.StepId).Single();
            var nsr = await _workflowRepository.GetAllNextStepRules();
            var nextStepId = nsr.Where(n => n.CurrentStepId == stepId && n.ConditionValue == "Submit").Select(n => n.NextStepId).Single();

            try
            {
                // create new Process
                var newProcess = new Process()
                {
                    WorkflowId = bookRequestWorkflowId,
                    RequesterId = user!.Id,
                    RequestType = "Book Request",
                    Status = "Pending",
                    RequestDate = DateTime.Now,
                    CurrentStepId = nextStepId
                };

                await _workflowRepository.CreateProcess(newProcess);

                // get latest process id
                var processes = await _workflowRepository.GetAllProcesses();
                var latestProcessId = processes.OrderByDescending(p => p.ProcessId).First().ProcessId;

                // create new Workflow Action
                var newWorkflowAction = new WorkflowAction()
                {
                    ProcessId = latestProcessId,
                    StepId = stepId,
                    ActorId = user.Id,
                    Action = "Book request submited",
                    ActionDate = DateTime.Now,
                    Comment = $"Requesting book titled {bookRequest.Title}"
                };

                await _workflowRepository.CreateWorkflowAction(newWorkflowAction);

                // create new Book Request
                var newBookRequest = new BookRequest()
                {
                    Title = bookRequest.Title,
                    ISBN = bookRequest.ISBN,
                    Author = bookRequest.Author,
                    Publisher = bookRequest.Publisher,
                    ProcessId = latestProcessId
                };

                await _workflowRepository.CreateBookRequest(newBookRequest);

                // get next role from workflow sequence
                var ws = await _workflowRepository.GetWorkflowSequenceById(newProcess.CurrentStepId);
                var nextRoleId = ws!.RequiredRoleId;

                var nextRole = await _roleManager.FindByIdAsync(nextRoleId!);

                // get next users from nextRole
                var nextUsers = await _userManager.GetUsersInRoleAsync(nextRole!.Name!);
                // get next users emails
                var userEmails = nextUsers.Select(u => u.Email).ToList();

                // send email to librarian and library user
                var emailTemplate = File.ReadAllText(@"./EmailTemplate/BookRequest.html");

                var emailBody = string.Format(emailTemplate,
                    $"{user.FirstName} {user.LastName}",
                    userName,
                    user.Email,
                    newBookRequest.Title,
                    newBookRequest.ISBN,
                    newBookRequest.Author,
                    newBookRequest.Publisher,
                    newProcess.Status
                );

                var mail = new EmailModel()
                {
                    EmailToIds = [user.Email],
                    EmailSubject = "Book Request Accepted",
                    EmailBody = emailBody,
                    EmailCCIds = userEmails!
                };

                await _emailService.SendEmail(mail);

                return true;
            }
            catch (Exception ex)
            {
                var a = ex;
                return false;
            }
        }

        // public async Task<bool> ReviewRequest(ReviewRequestModel reviewRequest)
        // {
        //     // get username from httpcontextaccessor
        //     var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        //     var user = await _userManager.FindByNameAsync(userName!);
        //     var userRoles = await _userManager.GetRolesAsync(user!);

        //     // get process next step required role
        //     var process = await _workflowRepository.GetProcessById(reviewRequest.ProcessId);

        //     if (process == null)
        //     {
        //         return false;
        //     }

        //     // get required role from current step in ws from process
        //     var requiredRoleId = process.CurrentStepIdNavigation.RequiredRoleId;

        //     if (requiredRoleId == null)
        //     {
        //         return false;
        //     }

        //     var requiredRole = await _roleManager.FindByIdAsync(requiredRoleId);

        //     if (!userRoles.Contains(requiredRole!.Name!))
        //     {
        //         return false;
        //     }

        //     if (reviewRequest.Action == "Approve")
        //     {
        //         // create new workflow action
        //         var newWorkflowAction = new WorkflowAction()
        //         {
        //             ProcessId = process.ProcessId,
        //             StepId = process.CurrentStepId,
        //             ActorId = user!.Id,
        //             Action = $"Book request approved by {requiredRole.Name}",
        //             ActionDate = DateTime.UtcNow,
        //             Comment = reviewRequest.Comment,
        //         };

        //         await _workflowRepository.UpdateWorkflowAction(newWorkflowAction);

        //         var nsrNextStepId = process.CurrentStepIdNavigation.NextStepIds.Where(nsr => nsr.CurrentStepId == process.CurrentStepId && nsr.ConditionValue == "Approve").Select(nsr => nsr.NextStepId).Single();

        //         // update process
        //         process.Status = "Reviewed";
        //         process.CurrentStepId = nsrNextStepId;
        //         await _workflowRepository.UpdateProcess(process);

        //         // get other actor emails
        //         var actions = await _workflowRepository.GetAllWorkflowActions();
        //         var actorEmails = actions.Where(e => e.ProcessId == process.ProcessId).Select(x => x.ActorIdNavigation.Email).ToList();
        //         // get requester email
        //         var requesterEmail = process.RequesterIdNavigation.Email;
        //         // remove requesterEmail from actorEmails so requester only receive the email once
        //         actorEmails.Remove(requesterEmail);

        //         // check if there is any next actor available
        //         // if exist, add actor email to actorEmails
        //         if (process.CurrentStepIdNavigation.RequiredRoleId != null)
        //         {
        //             var nextActorRoleName = process.CurrentStepIdNavigation.RequiredRoleIdNavigation.Name;
        //             var actors = await _userManager.GetUsersInRoleAsync(nextActorRoleName!);
        //             var nextActorEmails = actors.Select(a => a.Email).ToList();
        //             // append nextActorEmails to actorEmails
        //             actorEmails.AddRange(nextActorEmails);
        //         }

        //         // send email notification
        //         var emailTemplate = File.ReadAllText(@"./EmailTemplate/BookRequest.html");

        //         var emailBody = string.Format(emailTemplate,
        //             $"{process.RequesterIdNavigation.FirstName} {process.RequesterIdNavigation.LastName}",
        //             process.RequesterIdNavigation.UserName,
        //             process.RequesterIdNavigation.Email,
        //             process.BookRequestNavigation!.Title,
        //             process.BookRequestNavigation!.ISBN,
        //             process.BookRequestNavigation!.Author,
        //             process.BookRequestNavigation!.Publisher,
        //             process.Status
        //         );

        //         var mail = new EmailModel()
        //         {
        //             EmailToIds = [requesterEmail],
        //             EmailCCIds = actorEmails!,
        //             EmailSubject = "Book Request Progess",
        //             EmailBody = emailBody
        //         };

        //         await _emailService.SendEmail(mail);

        //         return true;
        //     }
        //     else if (reviewRequest.Action == "Reject")
        //     {
        //         // create new workflow action
        //         var newWorkflowAction = new WorkflowAction()
        //         {
        //             ProcessId = process.ProcessId,
        //             StepId = process.CurrentStepId,
        //             ActorId = user!.Id,
        //             Action = $"Book request rejected by {requiredRole.Name}",
        //             ActionDate = DateTime.UtcNow,
        //             Comment = reviewRequest.Comment,
        //         };

        //         await _workflowRepository.UpdateWorkflowAction(newWorkflowAction);

        //         var nsrNextStepId = process.CurrentStepIdNavigation.NextStepIds.Where(nsr => nsr.CurrentStepId == process.CurrentStepId && nsr.ConditionValue == "Reject").Select(nsr => nsr.NextStepId).Single();

        //         // update process
        //         process.Status = "Reviewed";
        //         process.CurrentStepId = nsrNextStepId;
        //         await _workflowRepository.UpdateProcess(process);

        //         // get other actor emails
        //         var actions = await _workflowRepository.GetAllWorkflowActions();
        //         var actorEmails = actions.Where(e => e.ProcessId == process.ProcessId).Select(x => x.ActorIdNavigation.Email).ToList();
        //         // get requester email
        //         var requesterEmail = process.RequesterIdNavigation.Email;
        //         // remove requesterEmail from actorEmails so requester only receive the email once
        //         actorEmails.Remove(requesterEmail);

        //         // check if any next actor available
        //         // if exist, add actor email to actorEmails
        //         if (process.CurrentStepIdNavigation.RequiredRoleId != null)
        //         {
        //             var nextActorRoleName = process.CurrentStepIdNavigation.RequiredRoleIdNavigation.Name;
        //             var actors = await _userManager.GetUsersInRoleAsync(nextActorRoleName!);
        //             var nextActorEmails = actors.Select(a => a.Email).ToList();
        //             // append nextActorEmails to actorEmails
        //             actorEmails.AddRange(nextActorEmails);
        //         }

        //         // send email notification
        //         var emailTemplate = File.ReadAllText(@"./EmailTemplate/BookRequest.html");

        //         var emailBody = string.Format(emailTemplate,
        //             $"{process.RequesterIdNavigation.FirstName} {process.RequesterIdNavigation.LastName}",
        //             process.RequesterIdNavigation.UserName,
        //             process.RequesterIdNavigation.Email,
        //             process.BookRequestNavigation!.Title,
        //             process.BookRequestNavigation!.ISBN,
        //             process.BookRequestNavigation!.Author,
        //             process.BookRequestNavigation!.Publisher,
        //             process.Status
        //         );

        //         var mail = new EmailModel()
        //         {
        //             EmailToIds = [requesterEmail],
        //             EmailCCIds = actorEmails!,
        //             EmailSubject = "Book Request Progess",
        //             EmailBody = emailBody
        //         };

        //         await _emailService.SendEmail(mail);

        //         return true;
        //     }
        //     else
        //     {
        //         return false;
        //     }
        // }

        public async Task<bool> ReviewRequest(ReviewRequestModel reviewRequest)
        {
            // get username from httpcontextaccessor
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var user = await _userManager.FindByNameAsync(userName!);
            var userRoles = await _userManager.GetRolesAsync(user!);

            // get process next step required role
            var process = await _workflowRepository.GetProcessById(reviewRequest.ProcessId);

            if (process == null)
            {
                return false;
            }

            // get required role from current step in ws from process
            var requiredRoleId = process.CurrentStepIdNavigation.RequiredRoleId;

            if (requiredRoleId == null)
            {
                return false;
            }

            var requiredRole = await _roleManager.FindByIdAsync(requiredRoleId);

            if (!userRoles.Contains(requiredRole!.Name!))
            {
                return false;
            }

            // create new workflow action
            var newWorkflowAction = new WorkflowAction()
            {
                ProcessId = process.ProcessId,
                StepId = process.CurrentStepId,
                ActorId = user!.Id,
                Action = reviewRequest.Action,
                ActionDate = DateTime.UtcNow,
                Comment = reviewRequest.Comment,
            };

            await _workflowRepository.UpdateWorkflowAction(newWorkflowAction);

            var nsrNextStepId = process.CurrentStepIdNavigation.NextStepIds.Where(nsr => nsr.CurrentStepId == process.CurrentStepId && nsr.ConditionValue == reviewRequest.Action).Select(nsr => nsr.NextStepId).Single();

            // update process
            process.Status = "Reviewed";
            process.CurrentStepId = nsrNextStepId;
            await _workflowRepository.UpdateProcess(process);

            // get other actor emails
            var actions = await _workflowRepository.GetAllWorkflowActions();
            var actorEmails = actions.Where(e => e.ProcessId == process.ProcessId).Select(x => x.ActorIdNavigation.Email).ToList();
            // get requester email
            var requesterEmail = process.RequesterIdNavigation.Email;
            // remove requesterEmail from actorEmails so requester only receive the email once
            actorEmails.Remove(requesterEmail);

            // check if there is any next actor available
            // if exist, add actor email to actorEmails
            if (process.CurrentStepIdNavigation.RequiredRoleId != null)
            {
                var nextActorRoleName = process.CurrentStepIdNavigation.RequiredRoleIdNavigation.Name;
                var actors = await _userManager.GetUsersInRoleAsync(nextActorRoleName!);
                var nextActorEmails = actors.Select(a => a.Email).ToList();
                // append nextActorEmails to actorEmails
                actorEmails.AddRange(nextActorEmails);
            }

            // send email notification
            var emailTemplate = File.ReadAllText(@"./EmailTemplate/BookRequest.html");

            var emailBody = string.Format(emailTemplate,
                $"{process.RequesterIdNavigation.FirstName} {process.RequesterIdNavigation.LastName}",
                process.RequesterIdNavigation.UserName,
                process.RequesterIdNavigation.Email,
                process.BookRequestNavigation!.Title,
                process.BookRequestNavigation!.ISBN,
                process.BookRequestNavigation!.Author,
                process.BookRequestNavigation!.Publisher,
                process.Status
            );

            var mail = new EmailModel()
            {
                EmailToIds = [requesterEmail],
                EmailCCIds = actorEmails!,
                EmailSubject = "Book Request Progess",
                EmailBody = emailBody
            };

            await _emailService.SendEmail(mail);

            return true;

        }
    }
}