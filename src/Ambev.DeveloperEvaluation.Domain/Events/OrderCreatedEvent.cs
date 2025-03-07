using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class OrderCreatedEvent(Order order) : IEvent
{
    public Order? Order { get; set; } = order;
}