namespace SimpleLibraryManagementSystemWebAPI.Models
{
    public class LendingDto
    {
        public int UserId { get; set; }

        public int[] Books { get; set; } = [];
    }
}