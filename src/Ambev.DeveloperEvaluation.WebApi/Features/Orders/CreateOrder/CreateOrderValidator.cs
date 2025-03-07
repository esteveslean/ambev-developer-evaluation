using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

public class CreateOrderValidator  : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.OrderNumber).NotEmpty().NotNull().Length(1, 10);
        RuleFor(x => x.Branch).NotEmpty().MaximumLength(100);
        
        RuleFor(x => x.Items).NotEmpty()
            .NotEmpty()
            .ForEach(item => item.SetValidator(new CreateOrderItemValidator()));
    }
}