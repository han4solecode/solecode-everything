using System.ComponentModel.DataAnnotations;

namespace StudentSystemAPI.Models
{
    public class Guru
    {
        [Key]
        public int GuruId { get; set; }

        [Required]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? MataKuliah { get; set; }

        public List<Kehadiran>? Kehadirans { get; set; }

    }
}
