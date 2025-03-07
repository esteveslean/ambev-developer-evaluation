using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class ItemCancelledEvent(OrderItem item) : IEvent
{
    public OrderItem? OrderItem { get; set; } = item;
}