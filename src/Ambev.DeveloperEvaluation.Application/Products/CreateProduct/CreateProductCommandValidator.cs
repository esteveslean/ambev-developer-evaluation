using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Title).NotEmpty().NotNull().Length(3, 50);
        RuleFor(product => product.Price).NotEmpty().NotNull().GreaterThan(0);
        RuleFor(product => product.Description).NotEmpty().NotNull().Length(5, 500);
        RuleFor(product => product.Category).NotEmpty().NotNull();
        RuleFor(product => product.Rating).NotNull().SetValidator(new RatingValidator());
    }
}