using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

public class GetCartCommand(Guid id) : IRequest<GetCartResponse>
{
    public Guid Id { get; set; } = id;
}