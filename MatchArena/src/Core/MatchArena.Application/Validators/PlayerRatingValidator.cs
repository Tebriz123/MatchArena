using FluentValidation;
using MatchArena.Application.DTOs.Ratings;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal class PlayerRatingValidator:AbstractValidator<PostRatingDto>
    {
        public PlayerRatingValidator()
        {
            RuleFor(x => x.Rating)
            .NotEmpty()
            .WithMessage("Rating cannot be empty.")
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(500)
                .WithMessage("Comment cannot exceed 500 characters.");
        }
    }
}
