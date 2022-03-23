using Hangfire;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using NotiCore.API.Helpers;
using NotiCore.API.Infraestructure.Common;
using NotiCore.API.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly string _host;
        private readonly string _address;
        private readonly string _password;
        private readonly int _port;
        private readonly IEncryptionService _encryptionService;
        public EmailService(IConfiguration configuration, IEncryptionService encryptionService = null)
        {
            _host = encryptionService
                .Decrypt(configuration
                .GetSection("MailerValues")
                .GetSection(PropertyConstants.MailHost).Value);
            
            _port = Convert.ToInt32(encryptionService
                .Decrypt(configuration
                .GetSection("MailerValues")
                .GetSection(PropertyConstants.MailPort).Value));

            _address = encryptionService.Decrypt(configuration[PropertyConstants.MailerAddress]);

            _password = encryptionService.Decrypt(configuration[PropertyConstants.MailerPassword]);
            
            _encryptionService = encryptionService;
        }
        [DisplayName("{0} News Letter")]
        [AutomaticRetry(Attempts = 1)]
        public async Task SendNewsLetterEmail(string userEmail, string firstName, ICollection<Article> articles, string baseAddress)
        {
            var encryptedMail = _encryptionService.Encrypt(userEmail);
            var url = $"{baseAddress}/Subscription/Deactivate?values={encryptedMail}";
            var template = TemplateHelper.GetNewsLetterTemplate(firstName, articles, url);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = template;
            
            var message = new MimeMessage();
            message.To.Add(MailboxAddress.Parse(userEmail));
            message.From.Add(MailboxAddress.Parse(_address));
            message.Subject = "Your Noticore briefing";
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = 
                    (sender, certificate, certChainType, errors) => true;

                await client.ConnectAsync(
                        _host,
                        _port,
                        true);

                await client.AuthenticateAsync(_address, _password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendEnrollmentMailAsync(string email, string name, string url)
        {
            var template = TemplateHelper.GetEmailEnrollmentTemplate(name, url);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = template;

            var message = new MimeMessage();
            message.To.Add(MailboxAddress.Parse(email));
            message.From.Add(MailboxAddress.Parse(_address));
            message.Subject = "Welcome to noticore";
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback =
                    (sender, certificate, certChainType, errors) => true;

                await client.ConnectAsync(
                        _host,
                        _port,
                        true);

                await client.AuthenticateAsync(_address, _password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
