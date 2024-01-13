using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
namespace BusinessLayer.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string email)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            mimeMessage.To.Add(MailboxAddress.Parse(email));
            mimeMessage.Subject = "Welcome Email";
            mimeMessage.Body = new TextPart(TextFormat.Html) { Text = "Welcome Email" };
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(mimeMessage);
            smtp.Disconnect(true);

        }
    }
}
