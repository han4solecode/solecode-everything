using System.Security.Claims;
using HRIS.Application.Contracts;
using HRIS.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetAllProject([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var projects = await _projectService.GetAllProjects(recordsPerPage, currentPage);

            return Ok(projects);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var project = await _projectService.GetProjectById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAdded = await _projectService.AddNewProject(project);

            if (!isAdded)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Projno }, project);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project inputProject)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _projectService.UpdateExistingProject(id, inputProject);

            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok(inputProject);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isDeptDeleted = await _projectService.DeleteProject(id);

                if (!isDeptDeleted)
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

        [Authorize(Roles = "Department Manager")]
        [HttpGet("info")]
        public async Task<IActionResult> GetAllProjectsByManager()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var projects = await _projectService.GetAllProjectsByManager(userId!);

            return Ok(projects);
        }

        [Authorize(Roles = "Department Manager")]
        [HttpPost("create")]
        public async Task<IActionResult> AddProjectByManager([FromBody] Project project)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var res = await _projectService.AddNewProjectByManager(userId!, project);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Projno }, project);
        }

        [Authorize(Roles = "Department Manager")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProjectByManager(int id, Project inputProject)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var res = await _projectService.UpdateExistingProjectByManager(userId!, id, inputProject);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }

        [Authorize(Roles = "Department Manager")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProjectByManager(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var res = await _projectService.DeleteProjectByManager(userId!, id);

            if (res.Status == "Error")
            {
                return BadRequest(res.Message);
            }

            return Ok(res);
        }
    }
}