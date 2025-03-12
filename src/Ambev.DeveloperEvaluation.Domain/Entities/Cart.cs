using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Cart(Guid userId) : BaseEntity
{
    public Guid UserId { get; private set; } = userId;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<CartItem> _products = [];
    
    public IReadOnlyCollection<CartItem> Products => _products;
    
    public ValidationResultDetail Validate()
    {
        var validator = new CartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public void Update(IEnumerable<CartItem> products)
    {
        _products.RemoveAll(x => products.All(y => y.ProductId != x.ProductId));

        foreach (CartItem item in products)
        {
            CartItem? foundItem = _products.Find(x => x.ProductId == item.ProductId);

            if (foundItem is not null)
            {
                foundItem.Update(item);
                continue;
            }
            
            _products.Add(item);
        }
        
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveProduct(Guid productId)
    {
        var product = _products.Find(p => p.ProductId == productId);
        _ = product ?? throw new KeyNotFoundException($"Product with ID {productId} not found in cart.");

        _products.Remove(product);
    }

    public void AddProduct(Guid productId, int quantity)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be greater than zero.");

        var existingProduct = _products.FirstOrDefault(p => p.ProductId == productId);
        if (existingProduct is not null)
            existingProduct.UpdateAmount(quantity);
        else
            _products.Add(new CartItem { Id = Guid.NewGuid(), ProductId = productId, Amount = quantity });
    }
}