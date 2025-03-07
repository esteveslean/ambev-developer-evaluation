﻿using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.OrderNumber).NotEmpty().NotNull().Length(1, 10);
        RuleFor(x => x.Branch).NotEmpty().MaximumLength(100);
        
        RuleFor(x => x.Items).NotEmpty()
            .NotEmpty()
            .ForEach(item => item.SetValidator(new OrderItemValidator()));
    }
}