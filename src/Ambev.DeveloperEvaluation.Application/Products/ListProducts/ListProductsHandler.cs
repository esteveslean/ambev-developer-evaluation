using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class GetAllPrListProductsHandleroductsHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<ListProductsCommand, PaginatedList<ListProductsResult>>
{
    public async Task<PaginatedList<ListProductsResult>> Handle(ListProductsCommand request, CancellationToken cancellationToken)
    {
        BaseSpecification<Product> spec = ProductSpecifications.ListProductsPaginated(request.Order, request.Page, request.Size);
        
        var products = await repository.ListAsync(spec, cancellationToken);

        return new PaginatedList<ListProductsResult>(mapper.Map<List<ListProductsResult>>(products), products.Count(), request.Page, request.Size);
    }
}