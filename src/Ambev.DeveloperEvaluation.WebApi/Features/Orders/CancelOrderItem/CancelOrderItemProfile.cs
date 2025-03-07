using Ambev.DeveloperEvaluation.Application.Orders.CancelOrderItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrderItem;

public class CancelOrderItemProfile : Profile
{
    public CancelOrderItemProfile()
    {
        CreateMap<CancelOrderItemRequest, CancelOrderItemCommand>().ReverseMap();
    }
}