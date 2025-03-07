
namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder;

public class GetOrderRequest (Guid id)
{
    public Guid Id { get; set; } = id;
}