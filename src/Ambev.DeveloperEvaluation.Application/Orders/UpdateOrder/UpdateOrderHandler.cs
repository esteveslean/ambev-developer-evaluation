using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.MessageBroker.Consumers;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderHandler(IOrderRepository repository, IMapper mapper, IMediator mediator) : IRequestHandler<UpdateOrderCommand, UpdateOrderResponse?>
{
    public async Task<UpdateOrderResponse?> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateOrderValidator();
        ValidationResult? validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        Order? order = await repository.GetByIdAsync(command.Id, cancellationToken);
        _ = order ?? throw new KeyNotFoundException($"Order with ID {command.Id} has not found");

        var items = mapper.Map<List<OrderItem>>(command.Items);
        
        order.Update(command.UserId, command.TotalAmount, command.Branch, command.IsCancelled);
        order.UpdateItems(items);
        
        Order orderDb = await repository.UpdateAsync(order, cancellationToken);
        
        await mediator.Publish(new OrderModifiedEvent(orderDb), cancellationToken);
        
        return mapper.Map<UpdateOrderResponse>(orderDb);
    }
}