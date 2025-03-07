using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem;

public class CancelOrderItemValidator : AbstractValidator<CancelOrderItemCommand>
{
    public CancelOrderItemValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.OrderItemId).NotEmpty();
    }
}