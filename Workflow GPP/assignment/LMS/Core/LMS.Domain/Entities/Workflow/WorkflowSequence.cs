using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities.Workflow
{
    public class WorkflowSequence
    {
        [Key]
        public int StepId { get; set; }

        // reference to Workflow
        public int WorkflowId { get; set; }
        public Workflow Workflow { get; set; } = null!;

        public int? StepOrder { get; set; }

        public string StepName { get; set; } = null!;

        // reference to AppRole
        public string RequiredRoleId { get; set; } = null!;
        [ForeignKey("RequiredRoleId")]
        public AppRole RequiredRoleIdNavigation { get; set; } = null!;

        // reference to WorkflowSequence as a NextStepId
        public int? NextStepId { get; set; }
        [ForeignKey("NextStepId")]
        public WorkflowSequence? NextStepIdNavigation { get; set; }

        // navigation to Request
        public ICollection<Request> Requests { get; set; } = [];

        // navigation to WorkflowAction
        public ICollection<WorkflowAction> WorkflowActions { get; set; } = [];
    }
}