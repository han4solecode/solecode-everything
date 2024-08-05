using System.ComponentModel.DataAnnotations;

namespace SimpleLibraryManagementSystemAPI_PosgreSQL.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        [RegularExpression(@"^(?=(?:[^0-9]*[0-9]){10}(?:(?:[^0-9]*[0-9]){3})?$)[\d-]+$", ErrorMessage = "ISBN is invalid")]
        public string? ISBN { get; set; }
    }
}