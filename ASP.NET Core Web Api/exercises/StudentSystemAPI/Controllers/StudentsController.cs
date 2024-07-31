using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystemAPI.Models;

namespace StudentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = [];

        [HttpGet]
        public async Task<IActionResult> Get()
        {   
            await Task.Delay(1500);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var student = students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            await Task.Delay(1000);

            students.Add(student);
            return Created($"students/{student.StudentId}", student);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id , [FromBody] Student inputStudent)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var newStudent = students.FirstOrDefault(s => s.StudentId == id);

            if (newStudent == null)
            {
                return NotFound();
            }

            newStudent.StudentId = id;
            newStudent.Name = inputStudent.Name;
            newStudent.Major = inputStudent.Major;
            newStudent.Age = inputStudent.Age;
            newStudent.Hobbies = inputStudent.Hobbies;

            return Ok(newStudent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var student = students.FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            students.Remove(student);
            return NoContent();
        }
    }
}
