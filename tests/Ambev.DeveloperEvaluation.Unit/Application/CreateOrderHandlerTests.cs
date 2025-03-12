using Ambev.DeveloperEvaluation.Application.Orders;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateOrderHandlerTests
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly CreateOrderHandler _handler;

    public CreateOrderHandlerTests()
    {
        _repository = Substitute.For<IOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        _mediator = Substitute.For<IMediator>();
        _handler = new CreateOrderHandler(_repository, _mapper, _mediator);
    }

    [Fact(DisplayName = "Given valid order When creating order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        CreateOrderCommand command = CreateOrderHandlerTestData.GenerateValidCommand();
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = command.OrderNumber,
            UserId = command.UserId,
            TotalAmount = command.TotalAmount,
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
        };

        foreach(CreateOrderItemDTO item in command.Items)
            order.AddItem(new OrderItem(item.ProductId, item.Amount, item.UnitPrice));

        var result = new CreateOrderResponse
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderNumber = order.OrderNumber,
            TotalAmount = order.TotalAmount,
            Branch = order.Branch,
            IsCancelled = order.IsCancelled
        };

        _mapper.Map<Order>(command).Returns(order);
        _mapper.Map<CreateOrderResponse>(order).Returns(result);

        _repository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>())
            .Returns(order);

        // When
        CreateOrderResponse createResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createResult.Should().NotBeNull();
        createResult.Id.Should().Be(order.Id);
        await _repository.Received(1).CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid order data When creating order Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateOrderCommand(); // Empty command will fail validation
    
        // When
        Func<Task<CreateOrderResponse>> act = () => _handler.Handle(command, CancellationToken.None);
    
        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
    
    [Fact(DisplayName = "Given valid command When handling Then maps command to cart entity")]
    public async Task Handle_ValidRequest_MapsCommandToCart()
    {
        // Given
        CreateOrderCommand command = CreateOrderHandlerTestData.GenerateValidCommand();
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = command.OrderNumber,
            UserId = command.UserId,
            TotalAmount = command.TotalAmount,
            Branch = command.Branch,
            IsCancelled = command.IsCancelled,
        };
        
        foreach(CreateOrderItemDTO item in command.Items)
            order.AddItem(new OrderItem(item.ProductId, item.Amount, item.UnitPrice));
    
        _mapper.Map<Order>(command).Returns(order);
    
        _repository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>())
            .Returns(order);
    
        // When
        await _handler.Handle(command, CancellationToken.None);
    
        // Then
        _mapper.Received(1).Map<Order>(Arg.Is<CreateOrderCommand>(c =>
            c.UserId == command.UserId &&
            c.OrderNumber == command.OrderNumber &&
            c.TotalAmount == command.TotalAmount &&
            c.IsCancelled == command.IsCancelled &&
            c.Branch == command.Branch &&
            c.Items == command.Items));
    }
}
