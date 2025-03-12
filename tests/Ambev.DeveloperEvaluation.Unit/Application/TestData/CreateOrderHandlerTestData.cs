using Ambev.DeveloperEvaluation.Application.Orders;
using Ambev.DeveloperEvaluation.Domain.Common;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;

public static class CreateOrderHandlerTestData
{
    private static readonly Faker<CreateOrderCommand> createOrderHandlerFaker = new Faker<CreateOrderCommand>()
        .RuleFor(u => u.UserId, f => Guid.NewGuid())
        .RuleFor(u => u.OrderNumber, f => f.Random.Number(100, 999).ToString())
        .RuleFor(u => u.Branch, f => f.Company.CompanyName())
        .RuleFor(u => u.TotalAmount, f => 0)
        .RuleFor(u => u.IsCancelled, f => false);

    private static readonly Faker<CreateOrderItemDTO> CreateOrderItemFaker = new Faker<CreateOrderItemDTO>()
        .RuleFor(u => u.ProductId, Guid.NewGuid)
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(10, 100))
        .RuleFor(u => u.Amount, f => f.Random.Int(1, 10));

    public static CreateOrderCommand GenerateValidCommand()
    {
        CreateOrderCommand? command = createOrderHandlerFaker.Generate();
        command.Items = CreateOrderItemFaker.Generate(10);

        return command;
    }
}