using Cubic.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(r => r.Trim().ToLower() == "admin" || r.Trim().ToLower() == "member")
                .WithMessage("Role must be Admin or Member");

        }
    }
}
