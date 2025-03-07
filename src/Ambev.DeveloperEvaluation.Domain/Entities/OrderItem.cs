using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class OrderItem : BaseEntity
{
    //EF CORE requirements
    public OrderItem()
    {
    }
    
    public OrderItem(Guid productId, int amount, decimal unitPrice)
    {
        CheckMaximumAmountRestrictions(amount);

        Amount = amount;
        UnitPrice = unitPrice;
        ProductId = productId;
    }
    
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
    
    public Order? Order { get; set; }
    
    public void CheckMaximumAmountRestrictions(int amount)
    {
        if (amount > 20)
            throw new InvalidOperationException("Cannot have more than 20 items per product");
    }
    
    public void RaiseDiscount()
    {
        Discount = 0m;

        if (Amount >= 4)
            Discount = 0.1m;
        
        if (Amount >= 10) // greater or equal to max limit 20
            Discount = 0.2m;
        
        TotalPrice = Amount * UnitPrice * (1 - Discount);
    }
    
    public void Update(OrderItem item)
    {
        CheckMaximumAmountRestrictions(item.Amount);

        ProductId = item.ProductId;
        Amount = item.Amount;
        UnitPrice = item.UnitPrice;
    }
}