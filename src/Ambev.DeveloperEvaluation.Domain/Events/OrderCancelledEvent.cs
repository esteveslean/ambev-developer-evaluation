﻿using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class OrderCancelledEvent(Order order) : IEvent
{
    public Order? Order { get; private set; } = order;
}