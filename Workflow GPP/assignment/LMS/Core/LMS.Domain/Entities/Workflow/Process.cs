using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities.Workflow
{
    public class Process
    {
        // [Key]
        // public int RequestId { get; set; }

        // public string RequestType { get; set; } = null!;

        // public string Status { get; set; } = null!;

        // public DateTime RequestDate { get; set; }

        // // reference to Workflow
        // public int WorkflowId { get; set; }
        // public Workflow WorkflowIdNavigation { get; set; } = null!;

        // // reference to AppUser
        // public string RequesterId { get; set; } = null!;
        // [ForeignKey("RequesterId")]
        // public AppUser RequesterIdNavigation { get; set; } = null!;

        // // reference to WorkflowSequence
        // public int CurrentStepId { get; set; }
        // [ForeignKey("CurrentStepId")]
        // public WorkflowSequence CurrentStepIdNavigation { get; set; } = null!;

        // // reference to Process
        // public int ProcessId { get; set; }
        // public Process ProcessIdNavigation { get; set; } = null!;

        // // navigation to WorkflowAction
        // public ICollection<WorkflowAction> WorkflowActions { get; set; } = [];

        [Key]
        public int ProcessId { get; set; }

        // reference to Workflow
        public int WorkflowId { get; set; }
        public virtual Workflow WorkflowIdNavigation { get; set; } = null!;

        // reference to AppUser
        public string RequesterId { get; set; } = null!;
        [ForeignKey("RequesterId")]
        public virtual AppUser RequesterIdNavigation { get; set; } = null!;

        public string? RequestType { get; set; }

        public string Status { get; set; } = null!;

        // reference to WorkflowSequence
        public int CurrentStepId { get; set; }
        [ForeignKey("CurrentStepId")]
        public virtual WorkflowSequence CurrentStepIdNavigation { get; set; } = null!;

        public DateTime RequestDate { get; set; }

        // navigation to BookRequest (one to one relationship)
        public virtual BookRequest? BookRequestNavigation { get; set; }

        // navigation to WorkflowAction
        public virtual ICollection<WorkflowAction> WorkflowActions { get; set; } = [];
    }
}