using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3).WithMessage("Title must be at least 3 characters.");

        RuleFor(x => x.Category)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3).WithMessage("Title must be at least 3 characters.");

        RuleFor(x => x.Rating).SetValidator(new RatingValidator());
    }
}