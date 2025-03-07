using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.MessageBroker.Consumers;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderEvent(IPublishEndpoint publishEndpoint) : IEventHandler<OrderModifiedEvent>
{
    public async Task Handle(OrderModifiedEvent notification, CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish<IOrderModified>(new { notification.Order }, cancellationToken);
    }
}