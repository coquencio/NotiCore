using NotiCore.API.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices.Implementation
{
    public class PropertiesService : IPropertiesService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly DataContext _context;
        public PropertiesService(IEncryptionService encryptionService, DataContext context)
        {
            _encryptionService = encryptionService;
            _context = context;
        }

        public string GetProperty(string propertyName)
        {
            var property = _context.Properties.Where(p => p.PropertyName.Equals(propertyName)).FirstOrDefault();
            if (property == null)
            {
                throw new Exception($"Property {propertyName} was not found");
            }
            return _encryptionService.Decrypt(property.PropertyValue);
        }

        public bool PropertyExist(string propertyName)
        {
            return _context.Properties.Count(p => p.PropertyName.Equals(propertyName)) > 0;
        }
        public void SaveProperty(string propertyName, string propertyValue)
        {
            var value = _encryptionService.Encrypt(propertyValue);
            var existingProperty = _context.Properties.Where(p => p.PropertyName.Equals(propertyName)).FirstOrDefault();
            if (existingProperty != null)
            {
                existingProperty.PropertyValue = value;
                _context.Update(existingProperty);
            }
            else
            {
                _context.Properties.Add(new Property()
                {
                    PropertyName = propertyName,
                    PropertyValue = value,
                });
            }
            _context.SaveChanges();
        }
        public void SaveRawProperty(string propertyName, string propertyValue)
        {
            var existingProperty = _context.Properties.Where(p => p.PropertyName.Equals(propertyName)).FirstOrDefault();
            if (existingProperty != null)
            {
                existingProperty.PropertyValue = propertyValue;
                _context.Update(existingProperty);
            }
            else
            {
                _context.Properties.Add(new Property()
                {
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                });
            }
            _context.SaveChanges();
        }
    }
}
