using FluentValidation;
using MatchArena.Application.DTOs.Colors;
using MatchArena.Application.DTOs.Player;
using MatchArena.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal class PostPlayerDtoValidator : AbstractValidator<PostPlayerDto>
    {
        public PostPlayerDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Name can be at most 50 characters.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MinimumLength(2).WithMessage("Surname must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Surname can be at most 50 characters.");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Age is required.")
                .InclusiveBetween(5, 60).WithMessage("Age must be between 5 and 60.");

            RuleFor(x => x.Height)
                .NotEmpty().WithMessage("Height is required.")
                .InclusiveBetween(100, 230).WithMessage("Height must be between 100 and 230 cm.");

            RuleFor(x => x.Information)
                .NotEmpty().WithMessage("Information is required.")
                .MinimumLength(10).WithMessage("Information must be at least 10 characters.")
                .MaximumLength(1000).WithMessage("Information can be at most 1000 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City can be at most 50 characters.");

            RuleFor(x => x.Position)
                .IsInEnum().WithMessage("Invalid player position.");

            RuleFor(x => x.Level)
                .IsInEnum().WithMessage("Invalid player level.");
            RuleFor(x => x.Photo)
                .NotNull().WithMessage("Image is required.")
                .Must(file => file!.ValidateType("image"))
                .WithMessage("Only image files are allowed.")
                .Must(file => file!.ValidateSize(FileSize.MB, 2))
                .WithMessage("Image size must be less than 2 MB.");
        }
    }

}
