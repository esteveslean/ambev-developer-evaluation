using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;


public class CancelOrderHandler(IOrderRepository repository, IMediator mediator) : IRequestHandler<CancelOrderCommand, bool>
{
    public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        Order? order = await repository.GetByIdAsync(command.Id, cancellationToken);
        _ = order ?? throw new KeyNotFoundException($"Order with ID {command.Id} not found.");

        order.Cancel();

        await repository.UpdateAsync(order, cancellationToken);
        await mediator.Publish(new OrderCancelledEvent(order), cancellationToken);
        
        return true;
    }
}