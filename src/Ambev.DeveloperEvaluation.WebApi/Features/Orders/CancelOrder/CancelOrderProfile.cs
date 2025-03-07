using Ambev.DeveloperEvaluation.Application.Orders.CancelOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CancelOrder;

public class CancelOrderProfile : Profile
{
    public CancelOrderProfile()
    {
        CreateMap<Guid, CancelOrderCommand>()
            .ConstructUsing(id => new CancelOrderCommand(id));
    }
}