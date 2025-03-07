using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;

public class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}