using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Security;
using System.Net;
using System.Net.Mail;

namespace Common
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> options) 
        {
            _emailSettings = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = _emailSettings.Email;
            var password = _emailSettings.Password;

            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };
            return client.SendMailAsync( new MailMessage(
               from: mail,
               to: email,
               subject,
               htmlMessage
                ));
        }
    }
}
