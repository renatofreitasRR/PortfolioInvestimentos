using Microsoft.AspNetCore.Hosting.Server;
using PortfolioInvestimentos.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Jobs.Email
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailSettings _emailSettings;
        public EmailSenderService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = _emailSettings.Mail;
            var pw = _emailSettings.Password;

            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            var mailMessage = new MailMessage(from: mail, to: email, subject, message);

            await client
                .SendMailAsync(
                   mailMessage
                );
        }
    }
}
