using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

public class ListProductsCommand  : IRequest<PaginatedList<ListProductsResult>>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string Order { get; set; } = string.Empty;
}