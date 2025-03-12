using System.Globalization;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class ProductTestData
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
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

    public static Product GenerateValid(int itemCount = 5)
    {
        return ProductFaker.Generate();
    }
}
