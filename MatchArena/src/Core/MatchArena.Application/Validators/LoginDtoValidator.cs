using FluentValidation;
using MatchArena.Application.DTOs.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.UsernameOrEmail)
                 .NotEmpty()
                    .WithMessage("UsernameOrEmail is required")
               .MinimumLength(4)
               .MaximumLength(256);
            RuleFor(r => r.Password)
               .NotEmpty()
               .MinimumLength(8);
        }
    }
}
