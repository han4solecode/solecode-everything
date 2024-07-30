using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Models
{
    public class Todo
    {
        public int ID { get; set; }

        [Required]
        public string? Task { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}