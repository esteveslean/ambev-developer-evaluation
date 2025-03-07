using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductCommand(Guid id) : IRequest<GetProductResult>
{
    public Guid Id { get; set; } = id;
}