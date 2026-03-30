using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HospitalManagementSystemAPIVersion.Proxy
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        // ناخد الإعدادات من DI
        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_settings.Email, _settings.Password),
                EnableSsl = true
            };

            using var mail = new MailMessage
            {
                From = new MailAddress(_settings.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(to);

            await client.SendMailAsync(mail);
        }
    }
}