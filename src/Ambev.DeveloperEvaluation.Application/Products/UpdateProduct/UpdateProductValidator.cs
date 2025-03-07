using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;


public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
        RuleFor(product => product.Title).NotEmpty().MaximumLength(100);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Description).NotEmpty().MaximumLength(500);
        RuleFor(product => product.Category).NotEmpty();
        RuleFor(product => product.Image).NotEmpty();
        RuleFor(product => product.Rating).NotNull().SetValidator(new RatingValidator());
    }
}