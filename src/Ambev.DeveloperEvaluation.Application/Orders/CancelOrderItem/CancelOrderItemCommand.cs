using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem;

public class CancelOrderItemCommand(Guid orderId, Guid orderItemId) : IRequest<bool>
{
    public Guid OrderId { get; init; } = orderId;
    public Guid OrderItemId { get; init; } = orderItemId;
}