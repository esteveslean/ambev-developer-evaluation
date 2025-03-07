using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartCommand : IRequest<UpdateCartResponse>
{
    public Guid Id { get; set; }
    public Guid UserId  { get; set; }
    public List<CartItemDTO> Products { get; set; } = new();
}