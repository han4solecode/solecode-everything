using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Entities.Workflow
{
    public class Workflow
    {
        [Key]
        public int WorkflowId { get; set; }

        public string WorkflowName { get; set; } = null!;

        public string? Description { get; set; }

        // navigation to WorkflowSequence
        public virtual ICollection<WorkflowSequence> WorkflowSequences { get; set; } = [];

        // navigation to Request
        // public ICollection<Request> Requests { get; set; } = [];

        // navigation to Process
        public virtual ICollection<Process> Processes { get; set; } = [];
    }
}