using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

public class GetOrderCommand(Guid id) : IRequest<GetOrderResponse>
{
    public Guid Id { get; set; } = id;
}