using FitnessTracker.Api.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FitnessTracker.Api.Services
{
    public static class EmailServiceErrors
    {
        public const string EmailSettingCannotBeNull = "Failed to send Email. {0} cannot be null";
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSetting _settings;
        
        public EmailService(IOptions<EmailSetting> settings)
        {
            _settings = settings.Value;

            if (string.IsNullOrEmpty(_settings.From))
            {
                throw new ArgumentNullException(nameof(_settings.From), string.Format(EmailServiceErrors.EmailSettingCannotBeNull, nameof(_settings.From)));
            }

            if (string.IsNullOrEmpty(_settings.SmtpServer))
            {
                throw new ArgumentNullException(nameof(_settings.SmtpServer), string.Format(EmailServiceErrors.EmailSettingCannotBeNull, nameof(_settings.SmtpServer)));
            }
        }

        public void SendEmail(string to, string subject, string body)
        {
            var mailMessage = CreateMessage(to, subject, body);

            var smtpClient = new SmtpClient(_settings.SmtpServer, _settings.Port)
            {
                Port = _settings.Port,
                EnableSsl = true,
                Credentials = new NetworkCredential(_settings.From, _settings.Password),
            };

            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
        }

        private MailMessage CreateMessage(string to, string subject, string body)
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.From = new MailAddress(_settings.From);
            mailMessage.Subject = subject;
            mailMessage.Body = $"<h4>{body}</h4>";

            return mailMessage;
        }
    }
}
