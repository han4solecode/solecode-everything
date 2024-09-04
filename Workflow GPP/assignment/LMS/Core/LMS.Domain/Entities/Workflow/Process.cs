using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Entities.Workflow
{
    public class Process
    {
        [Key]
        public int ProcessId { get; set; }

        public string ProcessName { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        // navigation to Request (one to one relationship)
        public Request? RequestNavigation { get; set; }
    }
}