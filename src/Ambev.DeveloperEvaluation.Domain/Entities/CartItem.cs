using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class CartItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
    public Guid CartId { get; set; }
    public Cart? Cart { get; set; }
    
    public void UpdateAmount(int amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero.");
        
        Amount = amount;
    }

    public void Update(CartItem item)
    {
        ProductId = item.ProductId;
        Amount = item.Amount;
    }
}