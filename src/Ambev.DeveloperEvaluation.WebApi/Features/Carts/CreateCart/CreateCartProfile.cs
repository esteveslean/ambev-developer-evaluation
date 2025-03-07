using Ambev.DeveloperEvaluation.Application.Carts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>();
        CreateMap<CreateCartResponse, Application.Carts.CreateCartResponse>().ReverseMap();
    }
}