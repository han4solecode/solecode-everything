using CSWebAPI.Application.Repositories;
using CSWebAPI.Application.Services.Interfaces;
using CSWebAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CSWebAPI.WebAPI.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAllProject([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var projects = await _projectService.GetAllProjects(recordsPerPage, currentPage);

            return Ok(projects);
        }

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

        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] Project project)
        {
            try
            {
                await _projectService.AddNewProject(project);

                return Created($"api/v1/project/{project.Projno}", project);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project inputProject)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var projUpdated = await _projectService.UpdateExistingProject(id, inputProject);

                if (!projUpdated)
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
    }
}