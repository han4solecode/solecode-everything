namespace SLMS.Domain.DTOs
{
    public class LendingDto
    {
        public int UserId { get; set; }

        public int[] BookIds { get; set; } = []; 
    }
}