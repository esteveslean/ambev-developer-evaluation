using Ambev.DeveloperEvaluation.Application.Orders;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

public class CreateOrderProfile : Profile
{
    public CreateOrderProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderCommand>().ReverseMap();
        CreateMap<Application.Orders.CreateOrderResponse, CreateOrderResponse>().ReverseMap();
    }
}