using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices
{
    public interface ISubscriberService
    {
        Task EnrollAsync(AddSubscriberRequest request, string action);
        bool IsRegistered(string email);
        bool IsActive(string email);
        public void Deactivate(string email);
    }
}
