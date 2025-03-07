using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository(DefaultContext context) : IProductRepository
{
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Product? product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        context.Products.Remove(product);

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<IEnumerable<Product>> ListAsync(BaseSpecification<Product> spec, CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = context.Products.AsQueryable();
        
        if(spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        if (!string.IsNullOrEmpty(spec.GenericOrder))
            query = spec.GetGenericOrderingQuery(query);
        
        return await query.Skip((spec.Page - 1) * spec.Size)
            .Take(spec.Size)
            .ToListAsync(cancellationToken);
    }
}