using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders;

public class CreateOrderHandler(
    IOrderRepository repository,
    IMapper mapper)
    : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
{
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = mapper.Map<Order>(command);
        order.CalculateTotalAmount();

        var orderDb = await repository.CreateAsync(order, cancellationToken);
        return mapper.Map<CreateOrderResponse>(orderDb);
    }
}