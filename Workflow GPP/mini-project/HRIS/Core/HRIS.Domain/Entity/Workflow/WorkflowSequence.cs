using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRIS.Domain.Entity.Workflow
{
    public class WorkflowSequence
    {
        [Key]
        public int StepId { get; set; }

        // reference to Workflow
        public int WorkflowId { get; set; }
        public virtual Workflow Workflow { get; set; } = null!;

        public int? StepOrder { get; set; }

        public string StepName { get; set; } = null!;

        // reference to AppRole
        public string? RequiredRoleId { get; set; } = null!;
        [ForeignKey("RequiredRoleId")]
        public virtual AppRole RequiredRoleIdNavigation { get; set; } = null!;

        // navigation to Process
        public virtual ICollection<Process> Processes { get; set; } = [];

        // navigation to WorkflowAction
        public virtual ICollection<WorkflowAction> WorkflowActions { get; set; } = [];

        // navigation to NextStepRule CurrentStepId
        [InverseProperty("CurrentStepIdNavigation")]
        public virtual ICollection<NextStepRule> CurrentStepIds { get; set; } = [];

        // navigation to NextStepRule NextStepId
        [InverseProperty("NextStepIdNavigation")]
        public virtual ICollection<NextStepRule> NextStepIds { get; set; } = [];
    }
}