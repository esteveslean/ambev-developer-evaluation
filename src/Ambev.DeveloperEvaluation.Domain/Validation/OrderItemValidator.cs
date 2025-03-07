using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class OrderItemValidator : AbstractValidator<OrderItemDTO>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.UnitPrice).GreaterThan(0);
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}

public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemDTO>
{
    public CreateOrderItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.UnitPrice).GreaterThan(0);
        RuleFor(x => x.Amount).GreaterThan(0);
    }
}