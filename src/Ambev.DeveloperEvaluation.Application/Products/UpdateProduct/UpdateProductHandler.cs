using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<UpdateProductCommand, UpdateProductResult?>
{
    public async Task<UpdateProductResult?> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        Product? product = await repository.GetByIdAsync(command.Id, cancellationToken);
        _ = product ?? throw new KeyNotFoundException($"Product with ID {command.Id} has not found");

        product.Title = command.Title;
        product.Price = command.Price;
        product.Description = command.Description;
        product.Image = command.Image;
        product.Rating = command.Rating;
        product.UpdatedAt = DateTime.UtcNow;

        var updatedProduct = await repository.UpdateAsync(product, cancellationToken);

        return mapper.Map<UpdateProductResult>(updatedProduct);
    }
}