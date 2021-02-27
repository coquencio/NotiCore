using AutoMapper;
using FluentValidation;
using NotiCore.API.Helpers;
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
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;
        public UserService(DataContext context, IEncryptionService encryptionService, IMapper mapper)
        {
            _context = context;
            _encryptionService = encryptionService;
            _mapper = mapper;
        }

        public bool IsUserValid(string userName, string password)
        {
            userName = userName.Trim().ToLower();
            password = _encryptionService.Encrypt(password);
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName && u.Password == password && u.IsActive);
            return (user != null);
        }

        public void AddUser(AddUserRequest userRequest)
        {
            userRequest.UserName = userRequest.UserName.Trim().ToLower();
            var userValidator = new AddUserRequestValidator();
            userValidator.ValidateAndThrow(userRequest);

            var potentialDuplication = _context.Users
                .SingleOrDefault(u => u.UserName.Equals(userRequest.UserName));

            if (potentialDuplication != null)
                ErrorsHelper.ThrowValidationError("Username already registered", "A record with that user name is already registered", "UserName");


            userRequest.Password = _encryptionService.Encrypt(userRequest.Password);
            var user = _mapper.Map<User>(userRequest);
            user.IsActive = true;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void AddUser(string key, AddUserRequest userRequest)
        {
            userRequest.UserName = userRequest.UserName.Trim().ToLower();
            var userValidator = new AddUserRequestValidator();
            userValidator.ValidateAndThrow(userRequest);

            var burnedService = _context.Users.Count();
            if (burnedService != 0)
                ErrorsHelper.ThrowValidationError("Users already registered");

            if (key == null || !key.Equals(_encryptionService.GetKey()))
                ErrorsHelper.ThrowValidationError("Invalid key");

            userRequest.Password = _encryptionService.Encrypt(userRequest.Password);
            var user = _mapper.Map<User>(userRequest);
            user.IsActive = true;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
