using HRIS.Domain.Entity.Workflow;
using Microsoft.AspNetCore.Identity;

namespace HRIS.Domain.Entity
{
    public class AppRole : IdentityRole
    {
        // navigation to WorkflowSequence
        public ICollection<WorkflowSequence> WorkflowSequences { get; set; } = [];
    }
}