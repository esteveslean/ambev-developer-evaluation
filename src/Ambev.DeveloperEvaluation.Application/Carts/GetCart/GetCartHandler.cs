using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;


public class GetCartHandler(ICartRepository repository, IMapper mapper) : IRequestHandler<GetCartCommand, GetCartResponse?>
{
    public async Task<GetCartResponse?> Handle(GetCartCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await repository.GetByIdAsync(request.Id, cancellationToken);
        return cart == null ? null : mapper.Map<GetCartResponse>(cart);
    }
}