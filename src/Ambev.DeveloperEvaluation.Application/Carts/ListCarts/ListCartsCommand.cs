using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

public class ListCartsCommand : IRequest<PaginatedList<ListCartsResponse>>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string Order { get; set; } = string.Empty;
}