using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrder;

public class CancelOrderValidator : AbstractValidator<CancelOrderRequest>
{
    public CancelOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order ID is required");
    }
}