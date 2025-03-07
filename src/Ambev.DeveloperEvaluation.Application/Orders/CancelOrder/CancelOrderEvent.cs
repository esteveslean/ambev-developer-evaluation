using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.MessageBroker.Consumers;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;


public class CancelOrderEvent(IPublishEndpoint publishEndpoint) : IEventHandler<OrderCancelledEvent>
{
    public async Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish<IOrderCancelled>(new { notification.Order }, cancellationToken);
    }
}