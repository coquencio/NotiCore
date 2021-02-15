using AutoMapper.Configuration;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using NotiCore.API.Helpers;
using NotiCore.API.Models.DataContext;
using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotiCore.API.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly ISubscriberService _subscriberService;
        private readonly SigningCredentials _credentials;
        public TokenService (IUserService userService, ISubscriberService subscriberService, IEncryptionService encryptionService)
        {
            _userService = userService;
            _subscriberService = subscriberService;
            var key = Encoding.UTF8.GetBytes(encryptionService.GetKey());
            _credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            
        }

        public string GetSubscriberToken(string mail)
        {
            if(!_subscriberService.IsActive(mail))
                throw new KeyNotFoundException("User We could not find any user with those credentials");

            var claims = new[]
                {
                    new Claim("Subscriber", mail),
                };
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: _credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetUserToken(string userName, string password)
        {
            if (!_userService.IsUserValid(userName, password))
                throw new KeyNotFoundException("User We could not find any user with those credentials");

            var claims = new[]
                {
                    new Claim("Admin", userName),
                };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: _credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GetUserToken(AddUserRequest request)
        {
            return GetUserToken(request.UserName, request.Password);
        }
    }

}
