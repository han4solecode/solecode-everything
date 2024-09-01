namespace ForMigration
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly? DateDeleted { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? DeleteReason { get; set; }
    }
}