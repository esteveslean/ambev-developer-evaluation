using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;

public class UpdateOrderRequest
{
    public string OrderNumber { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemDTO> Items { get; set; } = new();
}