using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Common;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;


public static class CreateProductHandlerTestData
{
    
    private static readonly Faker<CreateProductCommand> createProductHandlerFaker = new Faker<CreateProductCommand>()
        .RuleFor(u => u.Title, f => f.Commerce.ProductName())
        .RuleFor(u => u.Description, f => f.Commerce.ProductDescription())
        .RuleFor(u => u.Category, f => f.Commerce.Department())
        .RuleFor(u => u.Image, f => f.Image.PlaceImgUrl())
        .RuleFor(u => u.Price, f => f.Finance.Amount(10, 100))
        .RuleFor(u => u.Rating, f => new RatingDTO
        {
            Count = f.Random.Number(1, 100),
            Rate = f.Random.Number(1, 5)
        });

    public static CreateProductCommand GenerateValidCommand()
    {
        return createProductHandlerFaker.Generate();
    }
}
