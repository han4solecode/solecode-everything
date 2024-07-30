using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string? Name { get; set; }

        [Range(0, 1000)]
        public int Price { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }
    }
}
