using StudentSystemAPI.Models;

namespace StudentSystemAPI.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudent();

        Student? GetStudentById(int id);

        Student? AddStudent(Student student);

        Student? UpdateStudent(int id, Student inputStudent);

        bool DeleteStudent(int id);
    }
}
