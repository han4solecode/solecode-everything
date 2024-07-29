using System.ComponentModel.DataAnnotations;
using webapi_controller.Models.Validations;

namespace webapi_controller.Models
{
    public class Shirt
    {
        public int ID { get; set; }

        [Required]
        public string? Brand { get; set; }

        public string? Color { get; set; }

        [Shirt_EnsureCorrectSizing]
        public int? Size { get; set; }

        [Required]
        public string? Gender { get; set; }

        public double? Price { get; set; }
    }
}