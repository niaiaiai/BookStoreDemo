using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailConfig = _configuration.GetSection("Email");

            SmtpClient smtpClient = new SmtpClient(emailConfig.GetValue<string>("FromHost"), emailConfig.GetValue<int>("FromPort"));
            smtpClient.EnableSsl = emailConfig.GetValue<bool>("EnableSsl");
            smtpClient.Credentials = new NetworkCredential(emailConfig.GetValue<string>("FromEmail"), emailConfig.GetValue<string>("FromEmailPassword"));

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailConfig.GetValue<string>("FromEmail"), emailConfig.GetValue<string>("DisplayName"));
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlMessage;

            return smtpClient.SendMailAsync(mailMessage);
        }
    }
}
