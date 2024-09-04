using LMS.Domain.Entities.Workflow;
using Microsoft.AspNetCore.Identity;

namespace LMS.Domain.Entities
{
    public class AppRole : IdentityRole
    {
        // navigation to WorkflowSequence
        public ICollection<WorkflowSequence> WorkflowSequences { get; set; } = [];
    }
}