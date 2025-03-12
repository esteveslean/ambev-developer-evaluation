using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class  OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(sale => sale.OrderNumber)
            .NotNull()
            .NotEmpty()
            .Length(1, 10);
       
        RuleFor(sale => sale.TotalAmount)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(sale => sale.Branch)
            .NotEmpty()
            .NotNull();
       
        RuleFor(x => x.Items)
            .NotEmpty()
            .Must(items => items != null && items.Count != 0)
            .ForEach(item => item.SetValidator(new OrderItemValidator()));
    }
}