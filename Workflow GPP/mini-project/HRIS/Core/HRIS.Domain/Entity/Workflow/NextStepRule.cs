using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRIS.Domain.Entity.Workflow
{
    public class NextStepRule
    {
        [Key]
        public int RuleId { get; set; }

        // reference to WorkflowSequence as CurrentStepId
        public int CurrentStepId { get; set; }
        [ForeignKey("CurrentStepId")]
        public WorkflowSequence CurrentStepIdNavigation { get; set; } = null!;

        // reference to WorkflowSequence as NextStepId
        public int NextStepId { get; set; }
        [ForeignKey("NextStepId")]
        public WorkflowSequence NextStepIdNavigation { get; set; } = null!;

        public string ConditionType { get; set; } = null!;

        public string ConditionValue { get; set; } = null!;
    }
}