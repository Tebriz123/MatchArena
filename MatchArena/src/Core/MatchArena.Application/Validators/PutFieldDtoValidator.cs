using FluentValidation;
using MatchArena.Application.DTOs.Fields;
using MatchArena.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal class PutFieldDtoValidator:AbstractValidator<PutFieldDto>
    {
        public PutFieldDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Field name is required.")
                .MaximumLength(100).WithMessage("Field name cannot exceed 100 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.PricePerHour)
                .GreaterThan(0)
                .WithMessage("Price per hour must be greater than zero.");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithMessage("Start time must be earlier than end time.");

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime)
                .WithMessage("End time must be later than start time.");

            RuleFor(x => x.FieldInformation)
                .NotEmpty().WithMessage("Field information is required.")
                .MinimumLength(10).WithMessage("Field information must be at least 10 characters.")
                .MaximumLength(2000).WithMessage("Field information cannot exceed 2000 characters.");

            RuleFor(x => x.PrimaryPhoto)
                .NotNull().WithMessage("Primary photo is required.")
                .Must(file => file.ValidateType("image"))
                .WithMessage("Photo must be an image file.")
                .Must(file => file.ValidateSize(FileSize.MB, 5))
                .WithMessage("Photo size must not exceed 5 MB.");

            RuleForEach(x => x.AdditionalPhotos)
                 .Must(file => file.ValidateType("image"))
                 .WithMessage("Additional photos must be image files.")
                .Must(file => file.ValidateSize(FileSize.MB, 5))
                .WithMessage("Each additional photo must not exceed 5 MB.");
        }
    }
}
