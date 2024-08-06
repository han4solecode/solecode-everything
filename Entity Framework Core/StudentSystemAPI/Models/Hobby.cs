using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystemAPI.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        /*[ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }*/

    }
}
