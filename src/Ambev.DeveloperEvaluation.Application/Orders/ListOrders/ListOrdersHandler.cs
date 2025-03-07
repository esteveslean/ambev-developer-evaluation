using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.ListOrders;

public class ListOrdersHandler(IOrderRepository repository, IMapper mapper) : IRequestHandler<ListOrdersCommand, PaginatedList<ListOrdersResponse>>
{
    public async Task<PaginatedList<ListOrdersResponse>> Handle(ListOrdersCommand request, CancellationToken cancellationToken)
    {
        BaseSpecification<Order> spec = OrderSpecifications.ListOrdersPaginated(request.Order, request.Page, request.Size);
        
        IEnumerable<Order> orders = await repository.ListAsync(spec, cancellationToken);

        return new PaginatedList<ListOrdersResponse>(mapper.Map<List<ListOrdersResponse>>(orders), orders.Count(), request.Page, request.Size);
    }
}