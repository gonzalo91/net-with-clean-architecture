using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zalo.Clean.Application.Contracts.Email;
using Zalo.Clean.Application.Modules.Email;

namespace Zalo.Clean.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        
        public EmailSettings emailSettings { get; }
        public EmailSender(IOptions<EmailSettings> emailSetting)
        {
            this.emailSettings = emailSetting.Value;
        }
        public async Task<bool> SendEmail(EmailMessage email)
        {
            var client = new SendGridClient(emailSettings.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress { 
                Email = emailSettings.FromAddress,
                Name  = emailSettings.FromName
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);

            var response = await client.SendEmailAsync(message);

            return response.IsSuccessStatusCode;
        }
    }
}
