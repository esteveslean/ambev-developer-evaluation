using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.DeleteOrder;

public class DeleteOrderValidator  : AbstractValidator<DeleteOrderRequest>
{
    public DeleteOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order ID is required");
    }
}