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
            RuleFor(s => s.FirstName).NotNull();

            RuleFor(s => s.LastName)
                .Must(n =>n.Length >= 2)
                .When(e => !string.IsNullOrEmpty(e.LastName));

            RuleFor(s => s.Email).NotNull();
            RuleFor(s => s.Email).EmailAddress();
        }
    }
}
