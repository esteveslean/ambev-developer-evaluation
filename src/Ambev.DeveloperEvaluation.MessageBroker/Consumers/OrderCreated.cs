using Ambev.DeveloperEvaluation.Domain.Entities;
using MassTransit;

namespace Ambev.DeveloperEvaluation.MessageBroker.Consumers;

public interface IOrderCreated : IBaseMessage
{
    public Order Order { get; set; }
}

public class OrderCreated : IConsumer<IOrderCreated>
{
    public Task Consume(ConsumeContext<IOrderCreated> ctx)
    {
        Console.WriteLine($"OrderCreated Received at {ctx.Message.CreatedAt}");
        return Task.CompletedTask;
    }
}