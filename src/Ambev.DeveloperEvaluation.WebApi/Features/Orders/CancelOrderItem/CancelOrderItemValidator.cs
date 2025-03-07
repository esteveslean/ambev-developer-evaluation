using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem;

public class CancelOrderItemValidator : AbstractValidator<CancelOrderItemRequest>
{
    public CancelOrderItemValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order ID is required");
        
        RuleFor(x => x.OrderItemId)
            .NotEmpty()
            .WithMessage("Order Item ID is required");
    }
}