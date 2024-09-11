using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Lending : BaseEntity
    {
        // user entity
        public string AppUserId { get; set; } = null!;
        public virtual AppUser? AppUser { get; set; } = null!;

        // book entity
        public int BookId { get; set; }
        public virtual Book? Book { get; set; } = null!;

        public DateOnly BorrowDate { get; set; }

        public DateOnly DueReturnDate { get; set; }

        public DateOnly? DateReturned { get; set; }

        public decimal? Penalty { get; set; }

        // public string Status { get; set; } = null!;
    }
}