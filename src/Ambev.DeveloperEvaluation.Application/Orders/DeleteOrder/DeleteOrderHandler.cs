using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;


public class DeleteOrderHandler(IOrderRepository repository)
    : IRequestHandler<DeleteOrderCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteOrderValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return await repository.DeleteAsync(command.Id, cancellationToken);
    }
}