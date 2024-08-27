using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class LibraryCard : BaseEntity
    {
        public string CardNumber { get; set; } = null!;

        // public DateOnly CreatedDate { get; set; }

        public DateOnly ExpiryDate { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}