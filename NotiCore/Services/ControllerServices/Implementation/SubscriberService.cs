using FluentValidation;
using Microsoft.AspNetCore.Mvc.Routing;
using NotiCore.API.Infraestructure.Validators;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.Requests;
using NotiCore.API.Services.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices.Implementation
{
    public class SubscriberService : ISubscriberService
    {
        private readonly DataContext _context;
        private readonly IUrlService _urlService;
        private readonly IEmailService _emailService;
        public SubscriberService(DataContext context, IUrlService urlService, IEmailService emailService)
        {
            _context = context;
            _urlService = urlService;
            _emailService = emailService;
        }
        public async Task EnrollAsync(AddSubscriberRequest request, string action)
        {
            var validator = new AddSubscriberValidator();
            validator.ValidateAndThrow(request);

            var avoidEmail = false;
            var mailTemplate = new Subscriber();
            if (!IsRegistered(request.Email))
            {
                var subscriber = new Subscriber
                {
                    Email = request.Email,
                    IsActive = false,
                    HasAuthorized = false, 
                    Name = request.Email,
                    LastName = request.LastName
                };
                mailTemplate = subscriber;
                _context.Subscribers.Add(subscriber);
            }
            else
            {
                var existingSubscriber = _context.Subscribers
                    .Where(s => s.Email.Equals(request.Email))
                    .FirstOrDefault();

                avoidEmail = (existingSubscriber.HasAuthorized && existingSubscriber.IsActive);
                if (!avoidEmail)
                {
                    existingSubscriber.Name = request.FirstName;
                    existingSubscriber.LastName = request.LastName;
                    _context.Update(existingSubscriber);
                    mailTemplate = existingSubscriber;
                }
            }
            _context.SaveChanges();

            if (!avoidEmail)
            {
                var link = _urlService.BuildUrl(action, DateTime.Now.AddMinutes(30), "Email", mailTemplate.Email);
                await _emailService.SendEnrollmentMailAsync(mailTemplate.Email, mailTemplate.Name, link); 
            }
        }
        public bool IsRegistered(string email)
        {
            var subscriber = _context.Subscribers.SingleOrDefault(s => s.Email.Equals(email));
            return (subscriber != null);
        }

        public bool IsActive(string email)
        {
            var subscriber = _context.Subscribers.SingleOrDefault(s => s.Email.Equals(email) && s.IsActive);
            return (subscriber != null);
        }
    }
}
