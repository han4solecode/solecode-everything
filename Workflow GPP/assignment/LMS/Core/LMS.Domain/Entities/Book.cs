using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Book : BaseEntity
    {
        // public int BookId { get ; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string ISBN { get; set;} = null!;

        public string Publisher { get; set; } = null!;

        public string? Description { get; set; }

        public string? Category { get; set; }

        public string? Language { get; set; }

        public string Location { get; set; } = null!;

        public DateOnly PurchaseDate { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Lending> Lendings { get; set; } = new List<Lending>();

        // delete stamp
        // antara begini aja atau dipakein base intity buat nyimpen is deleted ini
        // public bool IsDeleted { get; set; } = false;
        // public string? DeleteReason { get; set; }
    }
}