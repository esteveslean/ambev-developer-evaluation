using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.MessageBroker.Consumers;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem;

public class CancelOrderItemEvent(IPublishEndpoint publishEndpoint) : IEventHandler<ItemCancelledEvent>
{
    public async Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish<IItemCancelled>(new { notification.OrderItem }, cancellationToken);
    }
}