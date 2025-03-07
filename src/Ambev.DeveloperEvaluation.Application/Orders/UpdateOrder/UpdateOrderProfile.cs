using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderProfile : Profile
{
    public UpdateOrderProfile()
    {
        CreateMap<UpdateOrderCommand, Order>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<OrderItemDTO, OrderItem>();

        CreateMap<Order, UpdateOrderResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        CreateMap<OrderItem, OrderItemDTO>();
    }
}