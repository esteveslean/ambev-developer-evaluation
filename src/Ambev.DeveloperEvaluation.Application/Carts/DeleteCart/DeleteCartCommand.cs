using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public class DeleteCartCommand(Guid id) : IRequest<bool>
{
    public Guid Id { get; init; } = id;
}