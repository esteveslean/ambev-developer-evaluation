using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders;

public class CreateOrderProfile : Profile
{
    public CreateOrderProfile()
    {   
        CreateMap<CreateOrderCommand, Order>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<CreateOrderItemDTO, OrderItem>();

        CreateMap<Order, CreateOrderResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<OrderItem, OrderItemDTO>();
    }
}