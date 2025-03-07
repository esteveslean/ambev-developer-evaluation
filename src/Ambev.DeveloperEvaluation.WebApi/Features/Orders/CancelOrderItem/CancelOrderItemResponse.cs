namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem;

public class CancelOrderItemResponse
{
    public Guid OrderId { get; set; }
    public Guid OrderItemId { get; set; }
}