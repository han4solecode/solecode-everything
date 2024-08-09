using Asp.Versioning;
using CompanySystemWebAPI.Interfaces;
using CompanySystemWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystemWebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllProject([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var projects = await _projectService.GetAllProject(recordsPerPage, currentPage);

            return Ok(projects);
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
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

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AddProject([FromBody] Project project)
        {
            try
            {
                await _projectService.AddProject(project);

                return Created($"api/v1/project/{project.Projno}", project);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project inputProject)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var projUpdated = await _projectService.UpdateProject(id, inputProject);

                if (projUpdated == null)
                {
                    return NotFound();
                }

                return Ok(projUpdated);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var isProjDeleted = await _projectService.DeleteProject(id);

                if (!isProjDeleted)
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

        [HttpGet]
        [Route("no-employee")]
        [MapToApiVersion("2.0")]
        public async Task<IEnumerable<Project>> NoEmpProject()
        {
            var projects = await _projectService.NoEmpProject();

            return projects;
        }

        [HttpGet]
        [Route("it-hr-project")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> ITAndHRProjects()
        {
            var projects = await _projectService.ITAndHRProjects();

            return Ok(projects);
        }
    }
}