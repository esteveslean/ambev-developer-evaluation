using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
    }
}