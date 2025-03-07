using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.MessageBroker.Consumers;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Application.Orders;

public class CreateOrderEvent(IPublishEndpoint publishEndpoint) : IEventHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish<IOrderCreated>(new { notification.Order }, cancellationToken);
    }
}