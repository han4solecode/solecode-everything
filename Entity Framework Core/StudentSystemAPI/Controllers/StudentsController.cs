using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystemAPI.Interfaces;
using StudentSystemAPI.Models;

namespace StudentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // private static List<Student> students = [];
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Get()
        {   
            return Ok(_studentService.GetAllStudent());
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var student = _studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
           var newStudent = _studentService.AddStudent(student);

            if (newStudent == null)
            {
                return BadRequest();
            }

            return Created($"students/{newStudent.StudentId}", newStudent);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student inputStudent)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var updatedStudent = _studentService.UpdateStudent(id, inputStudent);

            if (updatedStudent == null)
            {
                return NotFound();
            }

            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var isStudentDeleted = _studentService.DeleteStudent(id);

            if (isStudentDeleted == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
