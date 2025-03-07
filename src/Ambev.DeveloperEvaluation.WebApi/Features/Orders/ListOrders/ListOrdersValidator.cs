using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.ListOrders;

public class ListOrdersValidator : AbstractValidator<ListOrdersRequest>
{
    public ListOrdersValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.Size).GreaterThan(0);
    }
}