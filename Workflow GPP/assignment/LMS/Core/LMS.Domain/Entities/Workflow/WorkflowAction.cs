using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities.Workflow
{
    public class WorkflowAction
    {
        [Key]
        public int ActionId { get; set; }

        public string Action { get; set; } = null!;

        public DateTime ActionDate { get; set; }

        public string? Comment { get; set; }

        // reference to Request
        public int RequestId { get; set; }
        public Request RequestIdNavigation { get; set; } = null!;

        // reference to WorkflowSequence
        public int StepId { get; set; }
        [ForeignKey("StepId")]
        public WorkflowSequence StepIdNavigation { get; set; } = null!;

        // reference to AppUser
        public string ActorId { get; set; } = null!;
        [ForeignKey("ActorId")]
        public AppUser ActorIdNavigation { get; set; } = null!;
    }
}