using LMS.Application.Contracts;
using LMS.Application.Persistance;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LMS.Application.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public WorkflowService(IWorkflowRepository workflowRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _workflowRepository = workflowRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<object>> GetProcessToReview()
        {
            // get username from httpcontextaccessor
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var user = await _userManager.FindByNameAsync(userName!);

            var userRoleName = await _userManager.GetRolesAsync(user!);

            var userRole = await _roleManager.FindByNameAsync(userRoleName[0]);

            var processes = await _workflowRepository.GetAllProcesses();

            var processToReview = processes.Where(p => p.CurrentStepIdNavigation.RequiredRoleId == userRole!.Id).Select(p => new {
                ProcessId = p.ProcessId,
                Workflow = p.WorkflowIdNavigation.WorkflowName,
                Requester = $"{p.RequesterIdNavigation.FirstName} {p.RequesterIdNavigation.LastName}",
                RequestDate = string.Format("{0:dddd, d MMMM yyyy}", p.RequestDate),
                Status = p.Status,
                CurrentStep = p.CurrentStepIdNavigation.StepName
            });

            return processToReview;
        }

        public async Task<int> GetProcessToReviewCount()
        {
            // get username from httpcontextaccessor
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var user = await _userManager.FindByNameAsync(userName!);

            var userRoleName = await _userManager.GetRolesAsync(user!);

            var userRole = await _roleManager.FindByNameAsync(userRoleName[0]);

            var processes = await _workflowRepository.GetAllProcesses();

            var processCount = processes.Where(p => p.CurrentStepIdNavigation.RequiredRoleId == userRole!.Id).Count();

            return processCount;
        }
    }
}