namespace LMS.Application.DTOs.Request
{
    public class ReviewRequestModel
    {
        public int ProcessId { get; set; }

        public string Action { get; set; } = null!;

        public string Comment { get; set; } = null!;
    }
}