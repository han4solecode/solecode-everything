using System.ComponentModel.DataAnnotations;

namespace Dummy.Entity
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Position { get; set; } = null!;

        [Required]
        public int Level { get; set; }

        public string EmploymentType { get; set; } = null!;

        
    }
}