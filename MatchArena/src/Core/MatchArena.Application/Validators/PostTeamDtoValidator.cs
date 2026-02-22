using FluentValidation;
using MatchArena.Application.DTOs.Teams;
using MatchArena.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal class PostTeamDtoValidator:AbstractValidator<PostTeamDto>
    {
            public PostTeamDtoValidator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Team name is required.")
                    .MaximumLength(50).WithMessage("Team name cannot exceed 50 characters.");

                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("City is required.")
                    .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");

                RuleFor(x => x.MaxPlayer)
                    .InclusiveBetween(5, 25)
                    .WithMessage("Maximum players must be between 5 and 25.");

                RuleFor(x => x.Information)
                    .NotEmpty().WithMessage("Team information is required.")
                    .MinimumLength(10).WithMessage("Team information must be at least 10 characters.")
                    .MaximumLength(1000).WithMessage("Team information cannot exceed 1000 characters.");
                RuleFor(x => x.Photo)
                    .NotNull().WithMessage("Team photo is required.")
                    .Must(file => file.ValidateType("image"))
                    .WithMessage("Photo must be an image file (JPG, JPEG or PNG).")
                    .Must(file => file.ValidateSize(FileSize.MB, 5))
                    .WithMessage("Photo size must not exceed 5 MB.");

        }
    }
}
