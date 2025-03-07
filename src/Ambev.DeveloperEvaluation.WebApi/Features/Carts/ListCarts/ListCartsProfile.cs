using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;

public class ListCartsProfile : Profile
{
    public ListCartsProfile()
    {
        CreateMap<ListCartsRequest, ListCartsCommand>();
        CreateMap<ListCartsResponse, Application.Carts.ListCarts.ListCartsResponse>();
        CreateMap<Application.Carts.ListCarts.ListCartsResponse, ListCartsResponse>();
    }
}