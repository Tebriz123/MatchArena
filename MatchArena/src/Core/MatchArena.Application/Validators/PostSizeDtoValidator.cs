using FluentValidation;
using MatchArena.Application.DTOs.Sizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    public class PostSizeDtoValidator : AbstractValidator<PostSizeDto>
    {
        private const int MAX_LIMIT = 50;
        private const int MIN_LIMIT = 2;
        public PostSizeDtoValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Name is required")
                .MaximumLength(MAX_LIMIT)
                    .WithMessage("Name must be less than 50 characters")
                .MinimumLength(MIN_LIMIT)
                    .WithMessage("Name must be more than 1 characters")
                .Matches(@"^[A-Za-z0-9\s]$");



        }
    }
}
