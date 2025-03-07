using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartRepository(DefaultContext context) : ICartRepository
{
    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Carts
            .Include(cart => cart.Products)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await context.Carts.AddAsync(cart, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        context.Carts.Update(cart);
        await context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<IEnumerable<Cart>> ListAsync(BaseSpecification<Cart> spec, CancellationToken cancellationToken = default)
    {
        IQueryable<Cart> query = context.Carts.AsQueryable();
        
        if(spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        if (!string.IsNullOrEmpty(spec.GenericOrder))
            query = spec.GetGenericOrderingQuery(query);
        
        return await query.Skip((spec.Page - 1) * spec.Size)
            .Take(spec.Size)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        context.Carts.Remove(cart);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}