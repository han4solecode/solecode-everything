using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Lending : BaseEntity
    {
        // user entity
        public string AppUserId { get; set; } = null!;
        public AppUser? AppUser { get; set; } = null!;

        // book entity
        public int BookId { get; set; }
        public Book? Book { get; set; } = null!;

        public DateOnly BorrowDate { get; set; }

        public DateOnly DueReturnDate { get; set; }

    }
}