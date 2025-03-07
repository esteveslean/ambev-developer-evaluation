using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class OrderRepository(DefaultContext context) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Order>> ListAsync(BaseSpecification<Order> spec, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = context.Orders.Include(x => x.Items).AsQueryable();
        
        if(spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        if (!string.IsNullOrEmpty(spec.GenericOrder))
            query = spec.GetGenericOrderingQuery(query);
        
        return await query.Skip((spec.Page - 1) * spec.Size)
            .Take(spec.Size)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Order? order = await GetByIdAsync(id, cancellationToken);
        if (order == null)
            return false;

        context.Orders.Remove(order);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}