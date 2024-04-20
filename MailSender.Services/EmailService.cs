using System.Net;
using System.Net.Mail;

namespace MailSender.Services
{
    public class EmailService
    {
        // SMTP Simple Mail Transfer Protocol
        private readonly SmtpClient _client;

        public EmailService(string userName, string password,
            string host, int port)
        {
            _client = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = true, // SSL támogatás
                Credentials = new NetworkCredential(userName, password),
            };
        }

        public async Task SendMailAsync(
            string from, string to, string? subject, string? body,
            string? fromName = null, string? toName = null,
            bool isHtml = true)
        {
            MailAddress _from = new(from, fromName);
            MailAddress _to = new(to, toName);
            MailMessage mail = new()
            {
                From = _from,
                To = { _to },
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };
            await _client.SendMailAsync(mail);
        }
    }
}
