using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductCommand(Guid id) : IRequest<DeleteProductResult>
{
    public Guid Id { get; init; } = id;
}