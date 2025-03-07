using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders;

public class CreateOrderCommand : IRequest<CreateOrderResponse>
{
    public string OrderNumber { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public Guid UserId { get; set; }
    public List<CreateOrderItemDTO> Items { get; set; } = new();
}