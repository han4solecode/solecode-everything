using HRIS.Application.DTOs.Email;

namespace HRIS.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailModel model);
    }
}