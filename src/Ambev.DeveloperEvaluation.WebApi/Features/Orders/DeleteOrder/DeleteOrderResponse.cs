using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.DeleteOrder;

public class DeleteOrderResponse
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemDTO> Items { get; set; } = new();
}