using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartValidator : AbstractValidator<CreateCartRequest>
{
    public CreateCartValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Products).NotEmpty();
        RuleForEach(x => x.Products).SetValidator(new CartItemValidator());
    }
}