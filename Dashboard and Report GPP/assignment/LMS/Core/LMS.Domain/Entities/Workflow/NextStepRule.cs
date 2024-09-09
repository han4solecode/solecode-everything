using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities.Workflow
{
    public class NextStepRule
    {
        [Key]
        public int RuleId { get; set; }

        // reference to WorkflowSequence as CurrentStepId
        public int CurrentStepId { get; set; }
        [ForeignKey("CurrentStepId")]
        public virtual WorkflowSequence CurrentStepIdNavigation { get; set; } = null!;

        // reference to WorkflowSequence as NextStepId
        public int NextStepId { get; set; }
        [ForeignKey("NextStepId")]
        public virtual WorkflowSequence NextStepIdNavigation { get; set; } = null!;

        public string ConditionType { get; set; } = null!;

        public string ConditionValue { get; set; } = null!;
    }
}