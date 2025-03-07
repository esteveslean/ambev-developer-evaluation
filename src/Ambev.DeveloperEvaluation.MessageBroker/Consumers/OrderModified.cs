using Ambev.DeveloperEvaluation.Domain.Entities;
using MassTransit;

namespace Ambev.DeveloperEvaluation.MessageBroker.Consumers;

public interface IOrderModified : IBaseMessage
{
    public Order Order { get; set; }
}

public class OrderModified : IConsumer<IOrderModified>
{
    public Task Consume(ConsumeContext<IOrderModified> ctx)
    {
        Console.WriteLine($"OrderModified Received at {ctx.Message.CreatedAt}");
        return Task.CompletedTask;
    }
}