using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderCommand()  : IRequest<UpdateOrderResponse>
{
    public Guid Id { get; set; } 
    public string OrderNumber { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemDTO> Items { get; set; } = new();
}