using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Infrastructure.ForgotPassword
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_config["Email:Username"]));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_config["Email:Host"], int.Parse(_config["Email:Port"]), false);
            await client.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendPasswordResetAsync(string to, string token)
        {
            var subject = "Şifrə Sıfırlama";
            var body = $@"
            <h2>Şifrə Sıfırlama</h2>
            <p>Aşağıdakı tokeni istifadə edərək şifrənizi sıfırlayın:</p>
            <p style='background:#f0f0f0; padding:10px; font-size:18px;'>
                <strong>{token}</strong>
            </p>
            <p>Bu token 1 saat ərzində etibarlıdır.</p>";

            await SendAsync(to, subject, body);
        }
    }
}
