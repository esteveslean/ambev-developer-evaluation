using Ambev.DeveloperEvaluation.Application.Orders.ListOrders;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.ListOrders;

public class ListOrdersProfile : Profile
{
    public ListOrdersProfile()
    {
        CreateMap<ListOrdersRequest, ListOrdersCommand>();
        CreateMap<ListOrdersResponse, Application.Orders.ListOrders.ListOrdersResponse>();
        CreateMap<Application.Orders.ListOrders.ListOrdersResponse, ListOrdersResponse>();
    }
}