using FluentValidation;
using MatchArena.Application.DTOs.Teams;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal class PutTeamDtoValidator:AbstractValidator<PutTeamDto>
    {
        public PutTeamDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Team name is required.")
                .MaximumLength(50).WithMessage("Team name cannot exceed 50 characters.");

            RuleFor(x => x.CaptainName)
                .NotEmpty().WithMessage("Captain name is required.")
                .MaximumLength(100).WithMessage("Captain name cannot exceed 100 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");

            RuleFor(x => x.Information)
                .NotEmpty().WithMessage("Team information is required.")
                .MinimumLength(10).WithMessage("Team information must be at least 10 characters.")
                .MaximumLength(1000).WithMessage("Team information cannot exceed 1000 characters.");

            //RuleFor(x => x.Photo)
            //    .NotNull().WithMessage("Team photo is required.")
            //    .Must(FileValidator.BeValidImage)
            //    .WithMessage("Photo must be a JPG, JPEG or PNG image.")
            //    .Must(f => FileValidator.BeValidSize(f, 5))
            //    .WithMessage("Photo size must not exceed 5 MB.");
        }
    }
}
