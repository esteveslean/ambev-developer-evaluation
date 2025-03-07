using Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.DeleteOrder;

public class DeleteOrderProfile : Profile
{
    public DeleteOrderProfile()
    {
       CreateMap<Guid, DeleteOrderCommand>()
            .ConstructUsing(id => new DeleteOrderCommand(id));
    }
}