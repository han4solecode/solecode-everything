using LMS.Application.DTOs.Email;

namespace LMS.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel model);
    }
}