using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

public class CreateOrderRequest
{
    public string OrderNumber { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public Guid UserId { get; set; }
    public List<CreateOrderItemDTO> Items { get; set; } = new();
}