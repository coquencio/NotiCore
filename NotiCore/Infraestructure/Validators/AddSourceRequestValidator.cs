using FluentValidation;
using NotiCore.API.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Validators
{
    public class AddSourceRequestValidator : AbstractValidator<AddSourceRequest>
    {
        public AddSourceRequestValidator()
        {
            RuleFor(s => s).NotNull();
            RuleFor(s => s.Url).NotNull();
            RuleFor(s => s.Name).NotNull()
                .Must(n => n.Length <= 100).WithMessage("Source Name can only contain up to 100 characters");
        }
    }
}
