namespace Ambev.DeveloperEvaluation.Domain.Common;

public class OrderItemDTO
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}

public class CreateOrderItemDTO
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public decimal UnitPrice { get; set; }
}