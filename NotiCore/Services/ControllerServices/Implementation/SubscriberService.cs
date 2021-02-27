using NotiCore.API.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices.Implementation
{
    public class SubscriberService : ISubscriberService
    {
        private readonly DataContext _context;
        public SubscriberService(DataContext context)
        {
            _context = context;
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
