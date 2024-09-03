namespace LMS.Application.DTOs.Email
{
    public class EmailModel
    {
        public string EmailToId { get; set; } = null!;
        public string EmailToName { get; set; } = null!;
        public string EmailSubject { get; set; } = null!;
        public string EmailBody { get; set; } = null!;
    }
}