using FluentValidation;
using MatchArena.Application.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    public class PutCategoryDtoValidator : AbstractValidator<PutCategoryDto>
    {
        private const int MAX_LIMIT = 150;
        private const int MIN_LIMIT = 2;
        public PutCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty()
                        .WithMessage("Name is required")
                    .MaximumLength(MAX_LIMIT)
                        .WithMessage("Name must be less than 150 characters")
                    .MinimumLength(MIN_LIMIT)
                        .WithMessage("Name must be more than 1 characters")
                    .Matches(@"^[A-Za-z0-9\s]$");

        }
    }
}
