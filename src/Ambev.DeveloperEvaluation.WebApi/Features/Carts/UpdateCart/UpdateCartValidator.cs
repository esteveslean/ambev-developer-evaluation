using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

public class UpdateCartValidator : AbstractValidator<UpdateCartRequest>
{
    public UpdateCartValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Products).NotEmpty();
        RuleForEach(x => x.Products).SetValidator(new CartItemValidator());
    }
}