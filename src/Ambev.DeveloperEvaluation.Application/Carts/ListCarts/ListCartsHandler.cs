using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;


public class ListCartsHandler(ICartRepository repository, IMapper mapper) : IRequestHandler<ListCartsCommand, PaginatedList<ListCartsResponse>>
{
    public async Task<PaginatedList<ListCartsResponse>> Handle(ListCartsCommand request, CancellationToken cancellationToken)
    {
        BaseSpecification<Cart> spec = CartSpecifications.ListCartsPaginated(request.Order, request.Page, request.Size);
        
        IEnumerable<Cart> items = await repository.ListAsync(spec, cancellationToken);

        return new PaginatedList<ListCartsResponse>(mapper.Map<List<ListCartsResponse>>(items), items.Count(), request.Page, request.Size);
    }
}