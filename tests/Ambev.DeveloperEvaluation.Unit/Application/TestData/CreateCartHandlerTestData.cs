using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Domain.Common;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain;


public static class CreateCartHandlerTestData
{

    private static readonly Faker<CreateCartCommand> createCartHandlerFaker = new Faker<CreateCartCommand>()
        .RuleFor(u => u.UserId, Guid.NewGuid);

    private static readonly Faker<CartItemDTO> CartItemFaker = new Faker<CartItemDTO>()
        .RuleFor(u => u.ProductId, Guid.NewGuid)
        .RuleFor(u => u.Amount, f=> f.Random.Int(1, 10));
    
    public static CreateCartCommand GenerateValidCommand()
    {
        var command = createCartHandlerFaker.Generate();
        command.Products = CartItemFaker.Generate(10);

        return command;
    }
}
