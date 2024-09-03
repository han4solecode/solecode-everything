using LMS.Application.Contracts;
using LMS.Application.DTOs.Email;
using LMS.Application.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LMS.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailOptions _mailOptions;

        public EmailService(IOptions<MailOptions> options)
        {
            _mailOptions = options.Value;
        }

        public async Task<bool> SendEmail(EmailModel model)
        {
            var emailMessage = CreateEmailMessage(model);
            var result = await Send(emailMessage);

            return result;
        }

        private MimeMessage CreateEmailMessage(EmailModel model)
        {
            var emailMessage = new MimeMessage();
            var emailFrom = new MailboxAddress(_mailOptions.Name, _mailOptions.EmailId);

            emailMessage.From.Add(emailFrom);

            var emailTo = new MailboxAddress(model.EmailToName, model.EmailToId);

            emailMessage.To.Add(emailTo);
            emailMessage.Subject = model.EmailSubject;

            var emailBodyBuilder = new BodyBuilder
            {
                // TextBody = model.EmailBody,
                // HtmlBody = "<h1>Hello </h1>" + model.EmailBody
                HtmlBody = model.EmailBody
            };

            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            return emailMessage;
        }

        private async Task<bool> Send(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_mailOptions.Host, _mailOptions.Port, _mailOptions.UseSSL);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_mailOptions.UserName, _mailOptions.Password);
                await client.SendAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                var a = ex;
                return false;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}