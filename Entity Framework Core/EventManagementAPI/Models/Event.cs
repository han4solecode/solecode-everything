using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string? Name { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public int MaxAttendees { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}
