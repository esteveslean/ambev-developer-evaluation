using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.ListOrders;

public class ListOrdersProfile : Profile
{
    public ListOrdersProfile()
    {
        CreateMap<Order, ListOrdersResponse>();
    }
}