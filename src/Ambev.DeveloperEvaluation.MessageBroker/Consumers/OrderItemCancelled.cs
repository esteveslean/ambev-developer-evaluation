using Ambev.DeveloperEvaluation.Domain.Common;
using MassTransit;

namespace Ambev.DeveloperEvaluation.MessageBroker.Consumers;

public interface IItemCancelled : IBaseMessage
{
    public OrderItemDTO OrderItem { get; set; }
}

public class OrderItemCancelled : IConsumer<IItemCancelled>
{
    public Task Consume(ConsumeContext<IItemCancelled> ctx)
    {
        Console.WriteLine($"OrderItemCancelled Received at {ctx.Message.CreatedAt}");
        return Task.CompletedTask;
    }
}