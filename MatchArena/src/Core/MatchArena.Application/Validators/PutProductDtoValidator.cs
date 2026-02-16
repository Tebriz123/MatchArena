using FluentValidation;
using MatchArena.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal class PutProductDtoValidator: AbstractValidator<PostProductDto>
    {
        public PutProductDtoValidator()
        {
            RuleFor(p => p.Name)
              .NotEmpty()
              .WithMessage("Name is required")
              .MinimumLength(2)
              .WithMessage("Name must be more than 1 characters")
              .MaximumLength(150)
              .WithMessage("Name must be less than 150 characters");

            RuleFor(p => p.Description)
                    .NotEmpty()
                .WithMessage("Description is required");
           

            RuleFor(p => p.Price)
                    .GreaterThan(0)
                    .WithMessage("Price must be more 0")
                    .LessThanOrEqualTo(999999.99m)
                    .WithMessage("Price must be max 999999.99");
            RuleFor(p => p.CategoryId)
                    .NotEmpty()
                    .WithMessage("Category Id is required")
                    .GreaterThan(0);

            RuleFor(p => p.ColorIds)
                    .NotEmpty()
                    .WithMessage("Color Ids is required")
                    .Must(cIds => cIds.Count > 0);
            RuleForEach(p => p.ColorIds)
                    .GreaterThan(0);

            RuleFor(p => p.SizeIds)
                    .NotEmpty()
                    .WithMessage("Size Ids is required")
                    .Must(sIds => sIds.Count > 0);
            RuleForEach(p => p.SizeIds)
                    .GreaterThan(0);
        }
    }
}
