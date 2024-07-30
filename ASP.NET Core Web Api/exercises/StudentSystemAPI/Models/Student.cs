using System.ComponentModel.DataAnnotations;

namespace StudentSystemAPI.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Student ID is required")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        public string? Name { get; set; }

        [AllowedValues("Computer Science", "Information System", "Computer Engineering", ErrorMessage = "Major has to be Computer Science, Information System, or Computer Engineering")]
        public string? Major { get; set; }

        [Range(1, 100, ErrorMessage = "Age has to be between 1 to 100")]
        public int Age { get; set; }

        [MinLength(1, ErrorMessage = "Minimum 1 hobby")]
        public List<string>? Hobbies { get; set; }
    }
}