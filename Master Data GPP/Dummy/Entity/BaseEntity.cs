namespace Dummy.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateOnly DateCreated { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly? DateUpdated { get; set; }
    }
}