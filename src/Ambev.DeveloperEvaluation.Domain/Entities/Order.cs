using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Order : BaseEntity
{
    public Guid? UserId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public List<OrderItem> Items { get; set; } = [];
    
    public void Update(Guid userId, decimal totalAmount, string branch, bool isCancelled)
    {
        UserId = userId;
        TotalAmount = totalAmount;
        Branch = branch;
        IsCancelled = isCancelled;
    }

    public void UpdateItems(IEnumerable<OrderItem> items)
    {
        Items.RemoveAll(x => items.All(y => y.ProductId != x.ProductId));

        foreach (OrderItem item in items)
        {
            OrderItem? foundItem = Items.Find(x => x.ProductId == item.ProductId);
            if (foundItem is not null)
            {
                foundItem.Update(item);
                continue;
            }
            
            Items.Add(item);
        }

        CalculateTotalAmount();
        UpdatedAt = DateTime.UtcNow;
    }

    public void CalculateTotalAmount()
    {
        Items.ForEach(x => x.RaiseDiscount());
        TotalAmount = Items.Sum(item => item.Amount);
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Order is already cancelled.");

        IsCancelled = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddItem(OrderItem item) => Items.Add(item);

    public void CancelItem(Guid itemId)
    {
        OrderItem? item = Items.FirstOrDefault(i => i.Id == itemId);
        _ = item ?? throw new KeyNotFoundException($"Item with ID {itemId} not found.");

        Items.Remove(item);

        if (Items.Count > 0)
            CalculateTotalAmount();
        else
            Cancel();
        
        UpdatedAt = DateTime.UtcNow;
    }
}