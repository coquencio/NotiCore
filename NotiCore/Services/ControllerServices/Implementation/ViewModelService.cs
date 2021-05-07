using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.ViewModel;
using NotiCore.API.Services.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices.Implementation
{
    public class ViewModelService : IViewModelService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly DataContext _context;
        private readonly ISubscriberService _subscriberService;
        public ViewModelService(IEncryptionService encryptionService, DataContext context, ISubscriberService subscriberService)
        {
            _encryptionService = encryptionService;
            _context = context;
            _subscriberService = subscriberService;
        }
        public void SaveUserChanges(IFormCollection values)
        {
            var user = values["User"];
            if (!string.IsNullOrEmpty(user))
            {
                var email = _encryptionService.Decrypt(user);
                var subscriber = _context.Subscribers.Where(s => s.Email.Equals(email)).FirstOrDefault();
                subscriber.HasAuthorized = true;
                subscriber.IsActive = true;
                _context.Update(subscriber);

                var sources = _context.Sources.Where(s => s.IsActive).ToList().Select(s => s.SourceId);
                var selectedSources = new List<int>();
                foreach (var sourceId in sources)
                {
                    if (!string.IsNullOrEmpty(values[sourceId.ToString()]))
                        selectedSources.Add(sourceId);
                }

                foreach (var selectedSource in selectedSources)
                {
                    var existingSource = _context.SourceSubscriptions
                        .Where(s => s.SourceId == selectedSource && s.SubscriberId == subscriber.SubscriberId)
                        .FirstOrDefault();

                    if (existingSource == null)
                    {
                        var newSubscription = new SourceSubscription
                        {
                            SubscriberId = subscriber.SubscriberId,
                            SourceId = selectedSource,
                            IsActive = true,
                        };
                        _context.Add(newSubscription);
                    }
                    else
                    {
                        existingSource.IsActive = true;
                        _context.Update(existingSource);
                    }
                }

                var deletedSource = _context.SourceSubscriptions.Where(s => s.SubscriberId == subscriber.SubscriberId && !selectedSources.Contains(s.SourceId));
                foreach (var deleted in deletedSource)
                {
                    deleted.IsActive = false;
                    _context.Update(deleted);
                }
                _context.SaveChanges();
            }
        }
        public SourceSetupViewModel GetUserSourceSetupModel(string values)
        {
            var vm = new SourceSetupViewModel();
            vm.Expired = true;
            if (string.IsNullOrEmpty(values))
                return vm;

            List<(string key, string value)> decryptedValues;
            try
            {
                decryptedValues = _encryptionService.GetKeys(values);
            }
            catch (Exception)
            {
                return vm;
            }

            var expiration = decryptedValues.Where(k => k.key.Equals("Expiration")).FirstOrDefault().value;
            var expirationDate = Convert.ToDateTime(expiration);
            var now = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy H:mm"));
            vm.Expired = (now >= expirationDate);

            var email = decryptedValues.Where(k => k.key.Equals("Email")).FirstOrDefault().value;
            if (_subscriberService.IsRegistered(email))
                vm.User = _encryptionService.Encrypt(email);

            var subscriber = _context.Subscribers.Where(s => s.Email.Equals(email)).FirstOrDefault();
            vm.Name = subscriber.Name;
            vm.Sources = GetUserSources(subscriber);
            return vm;
        }

        private List<(int sourceId, string displayName, bool selected)> GetUserSources(Subscriber subscriber)
        {
            var toReturn = new List<(int sourceId, string displayName, bool selected)>();
            var sources = _context.Sources
                .Where(s => s.IsActive)
                .Include(s => s.Language)
                .ToList();

            foreach (var source in sources)
            {
                var hasSource = _context.SourceSubscriptions
                    .Any(c => c.IsActive && c.SubscriberId == subscriber.SubscriberId && c.SourceId == source.SourceId);

                toReturn.Add((source.SourceId, $"{source.Name} - {source.Language.Abbreviation}", hasSource));
            }

            return toReturn;
        }


    }
}
