namespace ForMigration
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