namespace HRIS.Application.DTOs.LeaveRequest
{
    public class LeaveRequestDto
    {
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string LeaveType { get; set; } = null!;

        public string Reason { get; set; } = null!;
    }
}