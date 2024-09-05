using System.ComponentModel.DataAnnotations;

namespace HRIS.Domain.Entity.Workflow
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        public string EmployeeId { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string LeaveType { get; set; } = null!;

        public string Reason { get; set; } = null!;

        // reference to Process
        public int ProcessId { get; set; }
        public Process ProcessIdNavigation { get; set; } = null!;
    }
}