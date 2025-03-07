using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class CreateCartCommand : IRequest<CreateCartResponse>
{
    public Guid UserId { get; set; }
    public List<CartItemDTO> Products { get; set; } = [];
}