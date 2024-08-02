using System.ComponentModel.DataAnnotations;

namespace OnlineFoodOrderingSystemWebAPI.Models
{
    public class Menu
    {
        public int Id { get; set; }

        [Required]
        [Length(2, 100, ErrorMessage = "Name length must be between 2 to 100")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 to 100000")]
        public decimal Price { get; set; }

        [AllowedValues("Food", "Beverage", "Dessert")]
        public string Category { get; set; } = string.Empty;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 to 5")]
        public double Rating { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsAvailable { get; set; } = true;
    }
}