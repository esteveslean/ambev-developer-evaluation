using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IEvent;