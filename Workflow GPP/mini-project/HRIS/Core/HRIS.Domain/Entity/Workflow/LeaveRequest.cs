using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRIS.Domain.Entity.Workflow
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        // reference to Employee
        public string EmployeeId { get; set; } = null!;
        [ForeignKey("EmployeeId")]
        public virtual Employee EmployeeIdNavigation { get; set; } = null!;

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string LeaveType { get; set; } = null!;

        public string Reason { get; set; } = null!;

        // reference to Process
        public int ProcessId { get; set; }
        public virtual Process ProcessIdNavigation { get; set; } = null!;
    }
}