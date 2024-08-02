using System.ComponentModel.DataAnnotations;

namespace OnlineFoodOrderingSystemWebAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [Length(2, 100, ErrorMessage = "Name length must be between 2 to 100")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Email format is invalid")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Phone numebr format is invalid")]
        public string? PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = "Address exceed 200 character limit")]
        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}