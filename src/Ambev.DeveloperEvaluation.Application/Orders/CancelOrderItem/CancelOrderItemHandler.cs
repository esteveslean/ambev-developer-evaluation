using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem;


public class CancelOrderItemHandler(IOrderRepository repository, IMediator mediator) : IRequestHandler<CancelOrderItemCommand, bool>
{
    public async Task<bool> Handle(CancelOrderItemCommand command, CancellationToken cancellationToken)
    {
        Order? order = await repository.GetByIdAsync(command.OrderId, cancellationToken);
        _ = order ?? throw new KeyNotFoundException($"Order with ID {command.OrderId} not found.");

        OrderItem? orderItem = order.Items.FirstOrDefault(i => i.Id == command.OrderItemId);
        _ = orderItem ?? throw new KeyNotFoundException($"Order item with ID {command.OrderItemId} not found.");

        order.CancelItem(orderItem.Id);

        await repository.UpdateAsync(order, cancellationToken);
        await mediator.Publish(new ItemCancelledEvent(orderItem), cancellationToken);
        
        return true;
    }
}