using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Cart>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        CreateMap<CartItemDTO, CartItem>();

        CreateMap<Cart, CreateCartResponse>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        CreateMap<CartItem, CartItemDTO>();
    }
}