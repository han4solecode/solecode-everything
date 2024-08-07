using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentSystemAPI.Models
{
    public class Kehadiran
    {
        [Key]
        public int KehadiranId { get; set; }

        public DateTime Date {  get; set; }

        public string? Status { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        [ForeignKey("Guru")]
        public int GuruId { get; set; }
        public Guru? Guru { get; set; }
    }
}
