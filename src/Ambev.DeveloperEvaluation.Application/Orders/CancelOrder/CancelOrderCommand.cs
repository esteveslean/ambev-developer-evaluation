using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;

public class CancelOrderCommand(Guid id) : IRequest<bool>
{
    public Guid Id { get; init; } = id;
}