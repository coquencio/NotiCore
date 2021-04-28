using Hangfire;
using MailKit.Net.Smtp;
using MimeKit;
using NotiCore.API.Helpers;
using NotiCore.API.Models.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Properties = NotiCore.API.Infraestructure.Common.PropertyConstants;

namespace NotiCore.API.Services.CoreServices.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IPropertiesService _propertiesService;

        public EmailService(IPropertiesService propertiesService)
        {
            _propertiesService = propertiesService;
        }
        [DisplayName("{0} News Letter")]
        [AutomaticRetry(Attempts = 1)]
        public async Task SendNewsLetterEmail(string userEmail, string firstName, ICollection<Article> articles)
        {
            var template = TemplateHelper.GetNewsLetterTemplate(firstName, articles);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = template;
            
            var message = new MimeMessage();
            message.To.Add(MailboxAddress.Parse(userEmail));
            message.From.Add(MailboxAddress.Parse(_propertiesService.GetProperty(Properties.MailerAddress)));
            message.Subject = "Your Noticore briefing";
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = 
                    (sender, certificate, certChainType, errors) => true;

                await client.ConnectAsync(
                        _propertiesService.GetProperty(Properties.MailHost),
                        Convert.ToInt32(_propertiesService.GetProperty(Properties.MailPort)),
                        true);

                await client.AuthenticateAsync(
                    _propertiesService.GetProperty(Properties.MailerAddress),
                    _propertiesService.GetProperty(Properties.MailerPassword));

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
