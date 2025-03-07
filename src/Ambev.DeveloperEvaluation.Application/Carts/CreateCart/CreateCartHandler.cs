using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts;


public class CreateCartHandler(ICartRepository repository, IMapper mapper) : IRequestHandler<CreateCartCommand, CreateCartResponse>
{
    public async Task<CreateCartResponse> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = mapper.Map<Cart>(command);

        var createdProduct = await repository.CreateAsync(product, cancellationToken);

        return mapper.Map<CreateCartResponse>(createdProduct);
    }
}