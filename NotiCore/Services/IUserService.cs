using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services
{
    public interface IUserService
    {
        public bool IsUserValid(string userName, string password);
        void AddUser(AddUserRequest userRequest);
        void AddUser(string key, AddUserRequest userRequest);

    }
}