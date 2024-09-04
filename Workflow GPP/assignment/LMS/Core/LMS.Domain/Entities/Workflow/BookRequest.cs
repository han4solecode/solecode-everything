using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.Entities.Workflow
{
    public class BookRequest
    {
        [Key]
        public int BookRequestId { get; set; }

        // public string ProcessName { get; set; } = null!;

        // public string? Description { get; set; }

        // public DateTime StartDate { get; set; }

        // public DateTime EndDate { get; set; }

        public string? Title { get; set; }

        public string? ISBN { get; set; }

        public string? Author { get; set; }

        public string? Publisher { get; set; }

        // reference to Process
        public int ProcessId { get; set; }
        public Process ProcessIdNavigation { get; set; } = null!;


        // navigation to Request (one to one relationship)
        // public Request? RequestNavigation { get; set; }
    }
}