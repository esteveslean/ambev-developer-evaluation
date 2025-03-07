using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartRequest
{
    public Guid UserId { get; set; }
    public List<CartItemDTO> Products { get; set; } = [];
}