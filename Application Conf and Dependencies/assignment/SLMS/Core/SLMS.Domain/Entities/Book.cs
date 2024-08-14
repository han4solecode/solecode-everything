using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SLMS.Domain.Entities
{
    [Table("books")]
    public class Book
    {
        [Key]
        [Column("bookid")]
        public int Bookid { get; set; }

        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [Column("author")]
        [StringLength(255)]
        public string Author { get; set; } = null!;

        [Column("publicationyear")]
        public DateOnly Publicationyear { get; set; }

        [Column("isbn")]
        [StringLength(17)]
        [RegularExpression(@"^(?=(?:[^0-9]*[0-9]){10}(?:(?:[^0-9]*[0-9]){3})?$)[\d-]+$", ErrorMessage = "ISBN is invalid")]
        public string Isbn { get; set; } = null!;

        [InverseProperty("Book")]
        public virtual ICollection<Lending> Lendings { get; set; } = new List<Lending>();
    }
}