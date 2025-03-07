namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem;

public class CancelOrderItemRequest
{
    public Guid OrderId { get; set; }
    public Guid OrderItemId { get; set; }
}