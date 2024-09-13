using HRIS.Application.Contracts;
using HRIS.Application.DTOs;
using HRIS.Application.DTOs.Project;
using HRIS.Application.Persistance;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace HRIS.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<Employee> _userManager;

        public ProjectService(IProjectRepository projectRepository, UserManager<Employee> userManager)
        {
            _projectRepository = projectRepository;
            _userManager = userManager;
        }

        public async Task<bool> AddNewProject(Project project)
        {
            try
            {
                await _projectRepository.Create(project);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProject(int id)
        {
            var proj = await _projectRepository.GetById(id);

            if (proj == null)
            {
                return false;
            }

            await _projectRepository.Delete(proj);
            return true;
        }

        public async Task<IEnumerable<Project>> GetAllProjects(int a, int b)
        {
            var projs = await _projectRepository.GetAll(a, b);

            return projs;
        }

        public async Task<Project?> GetProjectById(int id)
        {
            var proj = await _projectRepository.GetById(id);

            return proj;
        }

        public async Task<bool> UpdateExistingProject(int id, Project inputProject)
        {
            var proj = await _projectRepository.GetById(id);

            if (proj == null)
            {
                return false;
            }

            proj.Projname = inputProject.Projname;
            proj.Deptno = inputProject.Deptno;

            await _projectRepository.Update(proj);

            return true;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsByManager(string userId)
        {
            var manager = await _userManager.FindByIdAsync(userId);

            var projects = await _projectRepository.GetAllNoPaging();

            var projectInDept = projects.Where(p => p.Deptno == manager!.Deptno);

            return projectInDept;
        }

        public async Task<BaseResponseDto> AddNewProjectByManager(string userId, Project project)
        {
            var manager = await _userManager.FindByIdAsync(userId);

            project.Deptno = manager!.Deptno;

            try
            {
                await _projectRepository.Create(project);
                return new BaseResponseDto 
                {
                    Status = "Success",
                    Message = "Project created successfully"
                };
            }
            catch (System.Exception)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Can't create new project"
                };
            }
        }

        public async Task<BaseResponseDto> UpdateExistingProjectByManager(string userId, int projNo, Project inputProject)
        {
            var manager = await _userManager.FindByIdAsync(userId);

            var projs = await _projectRepository.GetAllNoPaging();

            var projectInDept = projs.Where(p => p.Deptno == manager!.Deptno);

            var isAvailable = projectInDept.Any(p => p.Projno == projNo);

            if (!isAvailable)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Update project request denied. Please check your privilegdes."
                };
            }

            var projectToBeUpdated = await _projectRepository.GetById(projNo);

            projectToBeUpdated!.Projname = inputProject.Projname;

            await _projectRepository.Update(projectToBeUpdated);

            return new BaseResponseDto
            {
                Status = "Success",
                Message = "Project updated successfully"
            };
        }

        public async Task<BaseResponseDto> DeleteProjectByManager(string userId, int projNo)
        {
            var manager = await _userManager.FindByIdAsync(userId);

            var projs = await _projectRepository.GetAllNoPaging();

            var projectInDept = projs.Where(p => p.Deptno == manager!.Deptno);

            var isAvailable = projectInDept.Any(p => p.Projno == projNo);

            if (!isAvailable)
            {
                return new BaseResponseDto
                {
                    Status = "Error",
                    Message = "Delete project request denied. Please check your privilegdes."
                };
            }

            var projectToBeDeleted = await _projectRepository.GetById(projNo);

            await _projectRepository.Delete(projectToBeDeleted!);

            return new BaseResponseDto
            {
                Status = "Success",
                Message = "Project deleted successfully"
            };
        }

        public async Task<IEnumerable<ProjectReportResponseDto>> GetProjectReport()
        {
            var projects = await _projectRepository.GetAllNoPaging();

            var data = projects.Select(x => new ProjectReportResponseDto {
                ProjectNo = x.Projno,
                ProjectName = x.Projname,
                TotalHoursLogged = x.Worksons.Sum(w => w.Hoursworked),
                TotalEmployees = x.Worksons.Count,
                AverageHoursPerEmployee = x.Worksons.Average(w => w.Hoursworked)
            }).ToList();

            return data;
        }

        public async Task<byte[]> GenerateProjectReport()
        {
            var data = await GetProjectReport();

            var htmlContent = string.Empty;
            htmlContent += "<h1> Project Report </h1>";
            htmlContent += "<table>";
            htmlContent += "<tr><th>Project Id</th><th>Project Name</th><th>Total Hours Logged</th><th>Total Employees</th><th>Average Hours Worked Per Employee</th></tr>";

            data.ToList().ForEach(item => {
                htmlContent += "<tr>";
                htmlContent += $"<td> {item.ProjectNo} </td>";
                htmlContent += $"<td> {item.ProjectName} </td>";
                htmlContent += $"<td> {item.TotalHoursLogged} </td>";
                htmlContent += $"<td> {item.TotalEmployees} </td>";
                htmlContent += $"<td> {item.AverageHoursPerEmployee} </td>";
                htmlContent += "</tr>";
            });
            htmlContent += "<table>";

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