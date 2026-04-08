using FluentValidation;
using MatchArena.Application.DTOs.Tournaments;
using MatchArena.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal class PutTournamentDtoValidator:AbstractValidator<PutTournamentDto>
    {

        public PutTournamentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tournament name is required.")
                .MaximumLength(100).WithMessage("Tournament name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(20).WithMessage("Description must be at least 20 characters.")
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");

            RuleFor(x => x.Photo)
                  .NotNull().WithMessage("Tournament photo is required.")
                  .Must(file => file.ValidateType("image"))
                  .WithMessage("Photo must be an image file (JPG, JPEG or PNG).")
                  .Must(file => file.ValidateSize(FileSize.MB, 5))
                  .WithMessage("Photo size must not exceed 5 MB.");

            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Start date must be in the future.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be later than start date.");

            RuleFor(x => x.RegistrationDeadline)
                .LessThan(x => x.StartDate)
                .WithMessage("Registration deadline must be before the start date.");

            RuleFor(x => x.MaxTeams)
                .InclusiveBetween(2, 64)
                .WithMessage("Maximum teams must be between 2 and 64.");

           

            RuleFor(x => x.EntryFee)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Entry fee cannot be negative.");

            RuleFor(x => x.PrizeFund)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Prize fund cannot be negative.");


            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid tournament status.");
        }
    }
}
