using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRIS.Domain.Entity.Workflow
{
    public class Process
    {
        [Key]
        public int ProcessId { get; set; }

        // reference to Workflow
        public int WorkflowId { get; set; }
        public Workflow WorkflowIdNavigation { get; set; } = null!;

        // reference to Employee
        public string RequesterId { get; set; } = null!;
        [ForeignKey("RequesterId")]
        public Employee RequesterIdNavigation { get; set; } = null!;

        public string? RequestType { get; set; }

        public string Status { get; set; } = null!;

        // reference to WorkflowSequence
        public int CurrentStepId { get; set; }
        [ForeignKey("CurrentStepId")]
        public WorkflowSequence CurrentStepIdNavigation { get; set; } = null!;

        public DateTime RequestDate { get; set; }

        // navigation to LeaveRequest (one to one relationship)
        public LeaveRequest? LeaveRequestNavigation { get; set; }

        // navigation to WorkflowAction
        public ICollection<WorkflowAction> WorkflowActions { get; set; } = [];
    }
}