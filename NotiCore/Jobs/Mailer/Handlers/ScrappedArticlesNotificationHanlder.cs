using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NotiCore.API.Jobs.Notifications;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Services.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NotiCore.API.Jobs.Mailer.Commands
{
    public class ScrappedArticlesNotificationHanlder : INotificationHandler<ScrappedArticlesNotification>
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;
        private string _baseAddress;
        public ScrappedArticlesNotificationHanlder(DataContext context, IEmailService emailService, IPropertiesService propertiesService)
        {
            _emailService = emailService;
            _context = context;
            _baseAddress = propertiesService.GetProperty("BaseAddress");
        }

        public Task Handle(ScrappedArticlesNotification notification, CancellationToken cancellationToken)
        {
            var pendingArticles = _context.Articles.Where(a => !a.Sent)
                .Include(a => a.Source)
                .Include(a => a.Topic)
                .ToList();

            var subscribers = _context.Subscribers.Where(s => s.IsActive && s.HasAuthorized).ToList();
            foreach (var subscriber in subscribers)
            {
                var subscriptions = _context.SourceSubscriptions.Where(s => s.IsActive && s.SubscriberId == subscriber.SubscriberId).ToList();
                var subscriberArticles = pendingArticles
                    .Where(s => subscriptions.Select(sub => sub.SourceId)
                    .Contains(s.SourceId))
                    .ToList();

                var cleanSubscriberArticles = new List<Article>();
                foreach (var article in subscriberArticles)
                {
                    if (cleanSubscriberArticles.Count(a => a.Url.Equals(article.Url)) == 0)
                        cleanSubscriberArticles.Add(article);
                }
                if (cleanSubscriberArticles.Any())
                {
                    BackgroundJob.Enqueue(() => _emailService.SendNewsLetterEmail(subscriber.Email, subscriber.Name, cleanSubscriberArticles.ToArray(), _baseAddress));
                }
            }
            foreach (var article in pendingArticles)
            {
                article.Sent = true;
                _context.Update(article);
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
