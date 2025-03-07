using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

public class GetOrderHandler(IOrderRepository repository, IMapper mapper)
    : IRequestHandler<GetOrderCommand, GetOrderResponse>
{
    public async Task<GetOrderResponse> Handle(GetOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new GetOrderValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var order = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (order == null) throw new Exception($"Order with Id {command.Id} not found.");

        return mapper.Map<GetOrderResponse>(order);
    }
}