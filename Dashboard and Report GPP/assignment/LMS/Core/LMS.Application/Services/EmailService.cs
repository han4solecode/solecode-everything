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

            // Multiple email receiver logic
            if (model.EmailToIds != null && model.EmailToIds.Count != 0)
            {
                foreach (var to in model.EmailToIds)
                {
                    var emailTos = new MailboxAddress(to ,to);
                    emailMessage.To.Add(emailTos);
                }
            }

            // Multiple email cc logic
            if (model.EmailCCIds != null && model.EmailCCIds.Count != 0)
            {
                foreach (var cc in model.EmailCCIds)
                {
                    var emailCc = new MailboxAddress(cc ,cc);
                    emailMessage.Cc.Add(emailCc);
                }
            }

            // var emailTo = new MailboxAddress(model.EmailToName, model.EmailToId);

            // emailMessage.To.Add(emailTo);
            emailMessage.Subject = model.EmailSubject;

            var emailBodyBuilder = new BodyBuilder
            {
                // TextBody = model.EmailBody,
                // HtmlBody = "<h1>Hello </h1>" + model.EmailBody
                HtmlBody = model.EmailBody
            };

            // Email attachments logic
            // Must be placed under emailBodyBuilder instance
            // This logic is used to accept a form-data from an endpoint (IFormFileCollection data type)
            if (model.Attachments != null && model.Attachments.Any())
            {
                byte[] fileBytes;

                foreach (var attachment in model.Attachments)
                {
                    using var ms = new MemoryStream();
                    attachment.CopyTo(ms);
                    fileBytes = ms.ToArray();
                    
                    // pass attachment to emailBodyBuilder
                    emailBodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            // Email attachment logic
            // This logic is used to set a server generated file as an email attachment
            if (model.Files != null && model.Files.Count != 0)
            {
                foreach (var file in model.Files)
                {
                    // pass file to emailBodyBuilder
                    emailBodyBuilder.Attachments.Add(file);
                }
            }

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