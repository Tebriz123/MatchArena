using FluentValidation;
using MatchArena.Application.DTOs.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal class PostRatingValidator : AbstractValidator<PostRatingDto>
    {
        public PostRatingValidator()
        {
            RuleFor(x => x.Rating)
                .NotEmpty()
                .WithMessage("Rating not empty.")
                .InclusiveBetween(1, 5)
                .WithMessage("Rating should be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(500)
                .WithMessage("The comment cannot exceed 500 characters.");
        }
    }
}
