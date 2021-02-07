using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Helpers
{
    public static class ErrorsHelper
    {
        public static void ThrowValidationError(string message, string details = null, string field = null)
        {
            var failure = new ValidationFailure(field, details);
            var errors = new List<ValidationFailure> { failure };
            throw new ValidationException(message, errors);
        }
    }
}
