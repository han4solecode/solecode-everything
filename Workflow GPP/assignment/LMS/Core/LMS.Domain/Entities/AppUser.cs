using LMS.Domain.Entities.Workflow;
using Microsoft.AspNetCore.Identity;

namespace LMS.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        [PersonalData]
        public virtual LibraryCard? LibraryCard { get; set; }

        [PersonalData]
        public virtual ICollection<Lending> Lendings { get; set; } = new List<Lending>();

        [PersonalData]
        public decimal? Penalty { get; set; }

        // refresh token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // navigation to Request
        // public ICollection<Request> Requests { get; set; } = [];

        // navigation to Process
        public virtual ICollection<Process> Processes { get; set; } = [];

        // navigation to WorkflowAction
        public virtual ICollection<WorkflowAction> WorkflowActions { get; set; } = [];
    }
}