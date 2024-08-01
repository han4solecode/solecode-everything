using System.ComponentModel.DataAnnotations;

namespace OnlineFoodOrderingSystemWebAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [Length(2, 100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}