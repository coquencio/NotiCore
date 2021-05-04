using NotiCore.API.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices
{
    public interface IEmailService
    {
        Task SendNewsLetterEmail(string userEmail, string firstName, ICollection<Article> articles);
        Task SendEnrollmentMailAsync(string email, string name, string url);
    }
}
