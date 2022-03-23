using FluentValidation;
using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Validators
{
    public class AddSubscriberValidator: AbstractValidator<AddSubscriberRequest>
    {
        public AddSubscriberValidator()
        {
            RuleFor(s => s).NotNull();
            RuleFor(s => s.FirstName).Must(e=> !string.IsNullOrEmpty(e))
                .WithMessage("First Name is required");

            RuleFor(s => s.LastName)
                .Must(n =>n.Length >= 2)
                .When(e => !string.IsNullOrEmpty(e.LastName))
                .WithMessage("Last name must have at least 2 characters");

            RuleFor(s => s.Email).Must(e => !string.IsNullOrEmpty(e)).WithMessage("Email is required");
            RuleFor(s => s.Email).EmailAddress().WithMessage("Invalid Email format");
        }
    }
}
