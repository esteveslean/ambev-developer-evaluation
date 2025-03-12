using System.Globalization;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class CartTestData
{
    private static readonly Faker<Cart> CartFaker = new Faker<Cart>()
        .CustomInstantiator(f => new Cart(Guid.NewGuid()));
    
     private static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
            .CustomInstantiator(f => new CartItem(Guid.NewGuid(), f.Random.Int(1, 20)));

    public static Cart GenerateValid(int itemCount = 5)
    {
        Cart? cart = CartFaker.Generate();
        
        List<CartItem>? items = CartItemFaker.Generate(itemCount);
        
        foreach (CartItem product in items.Select(x =>
                     new CartItem(x.ProductId, x.Amount)
                     {
                         Id = Guid.NewGuid()
                     }))
            cart.AddProduct(product.Id, product.Amount);

        return cart;
    }
}
