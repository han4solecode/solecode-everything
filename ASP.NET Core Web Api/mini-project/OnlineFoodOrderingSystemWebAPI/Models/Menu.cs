using System.ComponentModel.DataAnnotations;

namespace OnlineFoodOrderingSystemWebAPI.Models
{
    public class Menu
    {
        public int Id { get; set; }

        [Required]
        [Length(2, 100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        [AllowedValues("Food", "Beverage", "Dessert")]
        public string Category { get; set; } = string.Empty;

        [Range(0, 5)]
        public double Rating { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsAvailable { get; set; } = true;
    }
}