using System.Globalization;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class OrderTestData
{
    private static readonly Faker<Order> OrderFaker = new Faker<Order>()
        .RuleFor(u => u.UserId, f => Guid.NewGuid())
        .RuleFor(u => u.OrderNumber, f => f.Random.Number(100, 999).ToString())
        .RuleFor(u => u.Branch, f => f.Company.CompanyName())
        .RuleFor(u => u.TotalAmount, f => 0)
        .RuleFor(u => u.IsCancelled, f => false);

    private static readonly Faker<OrderItem> OrderItemFaker = new Faker<OrderItem>()
        .CustomInstantiator(f => new OrderItem(Guid.NewGuid(), f.Random.Int(1, 20), f.Finance.Amount(10, 100)));

    public static Order GenerateValid(int itemCount = 5)
    {
        Order? order = OrderFaker.Generate();
        
        List<OrderItem>? items = OrderItemFaker.Generate(itemCount);

        foreach (OrderItem item in items.Select(x =>
                     new OrderItem(x.ProductId, x.Amount, x.UnitPrice)
                     {
                         Id = Guid.NewGuid()
                     }))
            order.AddItem(item);

        order.CalculateTotalAmount();

        return order;
    }
}
