using FluentValidation;
using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Validators
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(s => s).NotNull();
            RuleFor(s => s.UserName).NotNull()
                .Must(n => n.Length > 5);

            RuleFor(s => s.Password).NotNull()
                .Must(p => p.Length > 5)
                .Must(p => HasValidPassword(p)).WithMessage("Password must contain: at least 6 characters, an uppercase, an lowecase, a digit and a symbol");
        }
        private bool HasValidPassword(string pw)
        {
            var lowercase = new Regex("[a-z]+");
            var uppercase = new Regex("[A-Z]+");
            var digit = new Regex("(\\d)+");
            var symbol = new Regex("(\\W)+");

            return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw) && symbol.IsMatch(pw));
        }
    }
}
