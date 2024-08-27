using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class User : BaseEntity
    {
        // public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        // role antara begini atau bikin enum
        public string Position { get; set; } = null!;

        // buat card number ini mending dibikin entity sendiri apa langsungan disini aja yaa
        // public string CardNumber { get; set; } = null!;
        // public DateOnly CardExpiryDate { get; set; }

        public LibraryCard? LibraryCard { get; set; }
    }
}