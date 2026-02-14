using FluentValidation;
using MatchArena.Application.DTOs.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                    .WithMessage("Name is required")
                .MinimumLength(3)
                    .WithMessage("Name must be more than 2 characters")
                .MaximumLength(50)
                    .WithMessage("Name must be less than 50 characters")
                 .Matches(@"^[A-Za-z]*$");
            RuleFor(r => r.Surname)
               .NotEmpty()
                    .WithMessage("Surname is required")
               .MinimumLength(3)
                    .WithMessage("Surname must be more than 2 characters")
               .MaximumLength(50)
                    .WithMessage("Surname must be less than 50 characters")
                .Matches(@"^[A-Za-z]*$");
          
            RuleFor(r => r.Email)
               .NotEmpty()
                    .WithMessage("Email is required")
               .MinimumLength(4)
               .MaximumLength(256)
               .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            RuleFor(r => r.Username)
                .MinimumLength(4)
                    .WithMessage("Username is required")
                .MaximumLength(256)
                .Matches(@"^[A-Za-z0-9-._@+]*$");
            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(8);
            
        }
    }
}
