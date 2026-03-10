using Companies.Core.Business.Interfaces;
using Companies.Core.DataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Companies.Core.Business.Services
{
    public class EmailService(IOptions<EmailSettings> options,
    ILogger<EmailService> logger,
    IWebHostEnvironment env) :IEmailService
    {
        private readonly EmailSettings _settings = options.Value;
       
        public async Task SendCompanyCreatedEmail(string companyName)
        {

            var template = await LoadTemplate("InsertTemplate.html");

            template = template.Replace("{{CompanyName}}", companyName);

            await SendEmailAsync("Company Created", template);

            logger.LogInformation("Company creation email sent");
        }

        public async Task SendCompanyUpdatedEmail(string companyName)
        {
            var template = await LoadTemplate("UpdateTemplate.html");

            template = template.Replace("{{CompanyName}}", companyName);

            await SendEmailAsync("Company Updated", template);

            logger.LogInformation("Company update email sent");
        }

        private async Task<string> LoadTemplate(string fileName)
        {
            var path = Path.Combine(env.ContentRootPath, _settings.TemplatePath, fileName);

            return await File.ReadAllTextAsync(path);
        }

        private async Task SendEmailAsync(string subject, string body)
        {
            var password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
           
            var message = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(_settings.NotificationEmail);

            using var smtp = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, password),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}
