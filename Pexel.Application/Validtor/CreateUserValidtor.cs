using FluentValidation;
using Pexel.Core.DTOs.Users;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Validtor
{
    public class CreateUserValidtor : AbstractValidator<UserDto>
    {
        public CreateUserValidtor()
        {
            RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("First name is required.")
           .MaximumLength(50).WithMessage("First name is too long (max 50 characters).");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name is too long (max 50 characters).");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
