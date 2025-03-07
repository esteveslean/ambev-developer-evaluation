using Ambev.DeveloperEvaluation.Domain.Entities;
using MassTransit;

namespace Ambev.DeveloperEvaluation.MessageBroker.Consumers;


public interface IOrderCancelled : IBaseMessage
{
    public Order Order { get; set; }
}

public class OrderCancelled : IConsumer<IOrderCancelled>
{
    public Task Consume(ConsumeContext<IOrderCancelled> ctx)
    {
        Console.WriteLine($"OrderCancelled Received at {ctx.Message.CreatedAt}");
        return Task.CompletedTask;
    }
}