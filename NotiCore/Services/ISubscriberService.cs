using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services
{
    public interface ISubscriberService
    {
        bool IsRegistered(string email);
        bool IsActive(string email);
    }
}
