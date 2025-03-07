namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

public class GetCartRequest(Guid id)
{
    public Guid Id { get; set; } = id;
}