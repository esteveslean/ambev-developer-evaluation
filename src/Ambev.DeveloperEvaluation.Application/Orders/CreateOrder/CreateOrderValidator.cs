﻿using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.OrderNumber).NotEmpty().NotNull().Length(1, 10);
        RuleFor(x => x.Branch).NotEmpty().MaximumLength(100);
        
        RuleFor(x => x.Items)
            .NotEmpty()
            .Must(items => items != null && items.Count != 0)
            .ForEach(item => item.SetValidator(new CreateOrderItemValidator()));
    }
}