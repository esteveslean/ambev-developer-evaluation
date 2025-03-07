using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;


public class GetCartProfile : Profile
{
    public GetCartProfile()
    {
        CreateMap<Cart, GetCartResponse>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
            .ReverseMap();

        CreateMap<CartItem, CartItemDTO>().ReverseMap();
    }
}