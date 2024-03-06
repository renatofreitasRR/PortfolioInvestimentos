﻿using PortfolioInvestimentos.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioInvestimentos.Application.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "renato.freitas@uni9.edu.br";
            var pw = "jUtCwIfE359BHdFp";

            var client = new SmtpClient("smtp-relay.brevo.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            await client
                .SendMailAsync(
                    new MailMessage(from: mail, to: email, subject, message)
                );
        }
    }
}
