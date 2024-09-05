using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRIS.Domain.Entity.Workflow
{
    public class WorkflowAction
    {
        [Key]
        public int ActionId { get; set; }

        // reference to Process
        public int ProcessId { get; set; }
        public Process ProcessIdNavigation { get; set; } = null!;

        // reference to WorkflowSequence
        public int StepId { get; set; }
        [ForeignKey("StepId")]
        public WorkflowSequence StepIdNavigation { get; set; } = null!;

        // reference to Employee
        public string ActorId { get; set; } = null!;
        [ForeignKey("ActorId")]
        public Employee ActorIdNavigation { get; set; } = null!;

        public string Action { get; set; } = null!;

        public DateTime ActionDate { get; set; }

        public string? Comment { get; set; }
    }
}