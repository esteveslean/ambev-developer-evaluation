using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.ListOrders;

public class ListOrdersCommand : IRequest<PaginatedList<ListOrdersResponse>>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string Order { get; set; } = string.Empty;
}