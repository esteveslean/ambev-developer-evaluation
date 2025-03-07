using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.ListOrders;

public class ListOrdersValidator : AbstractValidator<ListOrdersCommand>
{
    public ListOrdersValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.Size).GreaterThan(0);
    }
}