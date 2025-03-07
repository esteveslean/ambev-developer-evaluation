using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder;

public class GetOrderProfile  : Profile
{
    public GetOrderProfile()
    {
        CreateMap<Guid, GetOrderCommand>()
            .ConstructUsing(id => new GetOrderCommand(id));
        CreateMap<Application.Orders.GetOrder.GetOrderResponse, GetOrderResponse>();
    }
}