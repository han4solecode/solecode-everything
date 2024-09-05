using HRIS.Application.Contracts;
using HRIS.Application.DTOs.Email;
using HRIS.Application.Options;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace HRIS.Application.Services
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

            if (model.EmailToIds != null && model.EmailToIds.Count != 0)
            {
                foreach (var to in model.EmailToIds)
                {
                    var emailTos = new MailboxAddress(to ,to);
                    emailMessage.To.Add(emailTos);
                }
            }

            if (model.EmailCCIds != null && model.EmailCCIds.Count != 0)
            {
                foreach (var cc in model.EmailCCIds)
                {
                    var emailCc = new MailboxAddress(cc ,cc);
                    emailMessage.Cc.Add(emailCc);
                }
            }

            emailMessage.Subject = model.EmailSubject;

            var emailBodyBuilder = new BodyBuilder
            {
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