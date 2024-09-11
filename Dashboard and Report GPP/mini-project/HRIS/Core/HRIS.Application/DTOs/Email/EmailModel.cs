namespace HRIS.Application.DTOs.Email
{
    public class EmailModel
    {
        public List<string> EmailToIds { get; set; } = [];
        public List<string> EmailCCIds { get; set; } = [];
        public string EmailSubject { get; set; } = null!;
        public string EmailBody { get; set; } = null!;
    }
}