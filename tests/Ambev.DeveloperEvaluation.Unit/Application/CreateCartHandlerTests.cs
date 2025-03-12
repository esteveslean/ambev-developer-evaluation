using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateCartHandlerTests
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _repository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCartHandler(_repository, _mapper);
    }

    [Fact(DisplayName = "Given valid cart When creating cart Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        CreateCartCommand command = CreateCartHandlerTestData.GenerateValidCommand();
        var cart = new Cart(command.UserId)
        {
            Id = Guid.NewGuid(),
        };
        
        foreach(CartItemDTO item in command.Products)
            cart.AddProduct(item.ProductId, item.Amount);

        var result = new CreateCartResponse
        {
            Id = cart.Id,
            UserId = cart.UserId
        };
        
        _mapper.Map<Cart>(command).Returns(cart);
        _mapper.Map<CreateCartResponse>(cart).Returns(result);

        _repository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
            .Returns(cart);

        // When
        CreateCartResponse createResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createResult.Should().NotBeNull();
        createResult.Id.Should().Be(cart.Id);
        await _repository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCartCommand(); // Empty command will fail validation
    
        // When
        Func<Task<CreateCartResponse>> act = () => _handler.Handle(command, CancellationToken.None);
    
        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }
    
    [Fact(DisplayName = "Given valid command When handling Then maps command to cart entity")]
    public async Task Handle_ValidRequest_MapsCommandToCart()
    {
        // Given
        CreateCartCommand command = CreateCartHandlerTestData.GenerateValidCommand();
        Guid userId = Guid.NewGuid();
        
        var cart = new Cart(userId)
        {
            Id = Guid.NewGuid()
        };
        
        foreach(CartItemDTO item in command.Products)
            cart.AddProduct(item.ProductId, item.Amount);
    
        _mapper.Map<Cart>(command).Returns(cart);
    
        _repository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
            .Returns(cart);
    
        // When
        await _handler.Handle(command, CancellationToken.None);
    
        // Then
        _mapper.Received(1).Map<Cart>(Arg.Is<CreateCartCommand>(c =>
            c.UserId == command.UserId &&
            c.Products == command.Products));
    }
}
