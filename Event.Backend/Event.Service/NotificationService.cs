using Event.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Event.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.From.Add(MailboxAddress.Parse(
                _configuration.GetSection("EmailSettings:EmailUser").Value
            ));

            mimeMessage.To.Add(MailboxAddress.Parse(email));

            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync
            (
                _configuration.GetSection("EmailSettings:EmailHost").Value,
                int.Parse(_configuration.GetSection("EmailSettings:Port").Value!),
                SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync
            (
                _configuration.GetSection("EmailSettings:EmailUser").Value,
                _configuration.GetSection("EmailSettings:EmailPassword").Value
            );

            await smtp.SendAsync(mimeMessage);

            await smtp.DisconnectAsync(true);
        }
    }
}
