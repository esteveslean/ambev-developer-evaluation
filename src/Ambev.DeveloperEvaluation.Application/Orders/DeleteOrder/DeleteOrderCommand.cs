using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;

public class DeleteOrderCommand(Guid id) : IRequest<bool>
{
    public Guid Id { get; init; } = id;
}