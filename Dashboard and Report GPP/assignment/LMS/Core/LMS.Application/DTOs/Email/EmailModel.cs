using Microsoft.AspNetCore.Http;

namespace LMS.Application.DTOs.Email
{
    public class EmailModel
    {
        public string EmailToId { get; set; } = null!;
        public List<string> EmailToIds { get; set; } = [];
        public List<string> EmailCCIds { get; set; } = [];
        public string EmailToName { get; set; } = null!;
        public string EmailSubject { get; set; } = null!;
        public string EmailBody { get; set; } = null!;
        public IFormFileCollection Attachments { get; set; } = null!; // form-data
        public List<string> Files { get; set; } = []; // server generated file (storing file directory)
    }
}