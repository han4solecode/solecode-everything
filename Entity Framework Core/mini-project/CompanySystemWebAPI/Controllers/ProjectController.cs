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

        /// <summary>
        /// Retrive all project data with pagination
        /// </summary>
        /// <param name="recordsPerPage"></param>
        /// <param name="currentPage"></param>
        /// <returns>A list of projects</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/project?recordsPerPage=3&amp;currentPage=1
        /// 
        /// </remarks>
        /// <response code="200">Returns a list of projects</response>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProject([FromQuery] int recordsPerPage, [FromQuery] int currentPage)
        {
            var projects = await _projectService.GetAllProject(recordsPerPage, currentPage);

            return Ok(projects);
        }

        /// <summary>
        /// Retrive a project data from its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An project data</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/project/2
        /// 
        /// </remarks>
        /// <response code="200">Returns an project data</response>
        /// <response code="400">If id is invalid</response>
        /// <response code="404">If the project does not exist</response>
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Create a new project
        /// </summary>
        /// <param name="project"></param>
        /// <returns>A newly created project</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/project
        ///     {
        ///         "projName": "Blockchain Technology in Finance: Applications and Challenges",
        ///         "deptNo": 3
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created project data</response>
        /// <response code="400">If POST request is unsuccessful</response>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Update an existing project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputProject"></param>
        /// <returns>A newly updated project</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/v1/project/5
        ///     {
        ///         "projName": "Yearly report",
        ///         "deptNo": 6
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly updated project data</response>
        /// <response code="400">If PUT request is unsuccessful</response>
        /// <response code="404">If the project does not exist</response>
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Delete an existing project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/v1/project/5
        /// 
        /// </remarks>
        /// <response code="204">Delete request is successful</response>
        /// <response code="400">Delete request is successful</response>
        /// <response code="404">If the project does not exist</response>
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Route("no-project")]
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