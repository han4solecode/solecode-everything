using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public string? TagName { get; set; }
    }
}
