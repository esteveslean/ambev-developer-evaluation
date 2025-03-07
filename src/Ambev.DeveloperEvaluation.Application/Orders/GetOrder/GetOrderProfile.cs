using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

public class GetOrderProfile : Profile
{
    public GetOrderProfile()
    {
        CreateMap<Order, GetOrderResponse>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();
    }
}