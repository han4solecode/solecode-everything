using Microsoft.EntityFrameworkCore;
using StudentSystemAPI.Data;
using StudentSystemAPI.Interfaces;
using StudentSystemAPI.Models;

namespace StudentSystemAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAllStudent()
        {
            return _context.Students.Include(s => s.Hobbies).ToList();
        }

        public Student? GetStudentById(int id)
        {
            var student = _context.Students.Include(s => s.Hobbies).FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return null;
            }

            return student;
        }

        public Student? AddStudent(Student student)
        {
            var inputStudent = new Student()
            {
                Name = student.Name,
                Age = student.Age,
                Major = student.Major,
                Hobbies = [],
            };

            List<Hobby> studentHobbies = [];

            if (student.Hobbies == null)
            {
                return null;
            }

            foreach (var item in student.Hobbies)
            {
                studentHobbies.Add(item);
            }

            inputStudent.Hobbies.AddRange(studentHobbies);
            _context.Students.AddRange(student);
            _context.SaveChanges();

            return student;
        }

        public Student? UpdateStudent(int id, Student intputStudent)
        {
            var student = GetStudentById(id);

            if (student == null)
            {
                return null;
            }

            student.Name = intputStudent.Name;
            student.Age = intputStudent.Age;
            student.Major = intputStudent.Major;
            student.Hobbies = intputStudent.Hobbies;

            _context.Students.Update(student);
            _context.SaveChanges();

            return student;
        }

        public bool DeleteStudent(int id)
        {
            var student = GetStudentById(id);

            if (student == null )
            {
                return false;
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return true;
        }
    }
}
