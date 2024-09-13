using HRIS.Application.Contracts;
using HRIS.Application.DTOs.Workflow;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace HRIS.Application.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public WorkflowService(IWorkflowRepository workflowRepository, IHttpContextAccessor httpContextAccessor, UserManager<Employee> userManager, RoleManager<AppRole> roleManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
            _userManager = userManager;
            _workflowRepository = workflowRepository;
        }

        public async Task<IEnumerable<object>> GetProcessToReview()
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            var user = await _userManager.FindByNameAsync(userName!);

            var userRoleName = await _userManager.GetRolesAsync(user!);

            var userRole = await _roleManager.FindByNameAsync(userRoleName.Last());

            var processes = await _workflowRepository.GetAllProcesses();

            if (userRoleName.Contains("Employee Supervisor"))
            {
                // var userRole = await _roleManager.FindByNameAsync("Employee Supervisor");
                var processToReviewBySupervisor = processes.Where(p => p.CurrentStepIdNavigation.RequiredRoleId == userRole!.Id && p.RequesterIdNavigation.Supervisorempno == user!.Id).Select(p => new
                {
                    ProcessId = p.ProcessId,
                    Workflow = p.WorkflowIdNavigation.WorkflowName,
                    Requester = $"{p.RequesterIdNavigation.Fname} {p.RequesterIdNavigation.Lname}",
                    RequestDate = string.Format("{0:dddd, d MMMM yyyy}", p.RequestDate),
                    Status = p.Status,
                    CurrentStep = p.CurrentStepIdNavigation.StepName
                }).ToList();

                return processToReviewBySupervisor;
            }
            
            var processToReview = processes.Where(p => p.CurrentStepIdNavigation.RequiredRoleId == userRole!.Id).Select(p => new
            {
                ProcessId = p.ProcessId,
                Workflow = p.WorkflowIdNavigation.WorkflowName,
                Requester = $"{p.RequesterIdNavigation.Fname} {p.RequesterIdNavigation.Lname}",
                RequestDate = string.Format("{0:dddd, d MMMM yyyy}", p.RequestDate),
                Status = p.Status,
                CurrentStep = p.CurrentStepIdNavigation.StepName
            }).ToList();

            return processToReview;
        }

        public async Task<IEnumerable<TotalLeavesResponseDto>> GetTotalLeavesTakenPerLeaveType(DateOnly startDate, DateOnly endDate)
        {
            var leaveRequests = await _workflowRepository.GetAllLeaveRequests();

            var data = leaveRequests.Where(l => l.StartDate >= startDate && l.EndDate <= endDate && l.ProcessIdNavigation.Status.Contains("Approved by HR Manager")).GroupBy(l => l.LeaveType).Select(g => new TotalLeavesResponseDto
            {
                LeaveType = g.Key,
                LeaveCount = g.Count()
            }).ToList();

            return data;
        }

        public async Task<byte[]> GenerateTotalLeavesTakenPerLeaveTypeReport(DateOnly startDate, DateOnly endDate)
        {
            var data = await GetTotalLeavesTakenPerLeaveType(startDate, endDate);

            var htmlContent = string.Empty;

            htmlContent += "<h1> Leave Request Report </h1>";
            htmlContent += "<h3> Total Leaves Taken Per Leave Type </h3>";
            htmlContent += $"<p> Date range: {string.Format("{0:dddd, d MMMM yyyy}", startDate)} - {string.Format("{0:dddd, d MMMM yyyy}", endDate)} </p>";
            htmlContent += "<table>";
            htmlContent += "<tr><th>Leave Type</th><th>Total</th></tr>";

            data.ToList().ForEach(item =>
            {
                htmlContent += "<tr>";
                htmlContent += $"<td> {item.LeaveType} </td>";
                htmlContent += $"<td> {item.LeaveCount} </td>";
                htmlContent += "</tr>";
            });
            htmlContent += "</table>";

            var document = new PdfDocument();
            var pdfConfig = new PdfGenerateConfig
            {
                PageOrientation = PageOrientation.Portrait,
                PageSize = PageSize.A4,
            };
            pdfConfig.SetMargins(10);

            var cssFile = File.ReadAllText(@"./ReportTemplates/style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssFile);

            PdfGenerator.AddPdfPages(document, htmlContent, pdfConfig, css);

            var ms = new MemoryStream();
            document.Save(ms, false);
            var bytes = ms.ToArray();

            return bytes;
        }
    }
}