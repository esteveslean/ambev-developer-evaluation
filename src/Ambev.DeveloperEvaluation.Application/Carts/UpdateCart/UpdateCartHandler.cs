using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

public class UpdateCartHandler(ICartRepository repository, IMapper mapper) : IRequestHandler<UpdateCartCommand, UpdateCartResponse?>
{
    public async Task<UpdateCartResponse?> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var cart = await repository.GetByIdAsync(command.Id, cancellationToken);
        _ = cart ?? throw new KeyNotFoundException($"Cart with ID {command.Id} has not found");

        IEnumerable<CartItem> items = command.Products.Select(p => new CartItem
            { Id = Guid.NewGuid(), ProductId = p.ProductId, Amount = p.Amount });

        cart.Update(items);

        var updatedCart = await repository.UpdateAsync(cart, cancellationToken);

        return mapper.Map<UpdateCartResponse>(updatedCart);
    }
}