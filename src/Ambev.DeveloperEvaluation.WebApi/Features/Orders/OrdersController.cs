using Ambev.DeveloperEvaluation.Application.Orders;
using Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.Application.Orders.ListOrders;
using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.DeleteOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.ListOrders;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CreateOrderResponse = Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder.CreateOrderResponse;
using CreateOrderValidator = Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder.CreateOrderValidator;
using DeleteOrderValidator = Ambev.DeveloperEvaluation.WebApi.Features.Orders.DeleteOrder.DeleteOrderValidator;
using GetOrderResponse = Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder.GetOrderResponse;
using GetOrderValidator = Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetOrder.GetOrderValidator;
using ListOrdersResponse = Ambev.DeveloperEvaluation.WebApi.Features.Orders.ListOrders.ListOrdersResponse;
using ListOrdersValidator = Ambev.DeveloperEvaluation.WebApi.Features.Orders.ListOrders.ListOrdersValidator;
using UpdateOrderResponse = Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder.UpdateOrderResponse;
using UpdateOrderValidator = Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateOrder.UpdateOrderValidator;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IMediator mediator, IMapper mapper) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateOrderResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponseError
            {
                Type = "Validation Error",
                Error = "Invalid input data",
                Detail = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty
            });

        var command = mapper.Map<CreateOrderCommand>(request);
        var response = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateOrderResponse>
        {
            Success = true,
            Message = "Order created successfully",
            Data = mapper.Map<CreateOrderResponse>(response)
        });
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetOrderRequest(id);

        var validator = new GetOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponseError
            {
                Type = "Validation Error",
                Error = "Invalid input data",
                Detail = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty
            });
        
        var command = mapper.Map<GetOrderCommand>(request.Id);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(mapper.Map<GetOrderResponse>(response));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<ListOrdersResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListOrders([FromQuery(Name = "_order")] string order = "",
        [FromQuery(Name = "_page")] int page = 1,
        [FromQuery(Name = "_size")] int size = 10, CancellationToken cancellationToken = default)
    {
        var request = new ListOrdersRequest
        {
            Page = page,
            Size = size,
            Order = order
        };

        var validator = new ListOrdersValidator();
        ValidationResult? validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = mapper.Map<ListOrdersCommand>(request);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(new PaginatedResponse<ListOrdersResponse>
        {
            Success = true,
            Message = "List orders retrieved successfully",
            CurrentPage = response.CurrentPage,
            TotalCount = response.TotalItems,
            TotalPages = response.TotalPages,
            Data = mapper.Map<List<ListOrdersResponse>>(response.Data.ToList())
        });
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteOrderRequest { Id = id };
        var validator = new DeleteOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = mapper.Map<DeleteOrderCommand>(request.Id);
        await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Order deleted successfully"
        });
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateOrderValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponseError
            {
                Type = "Validation Error",
                Error = "Invalid input data",
                Detail = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty
            });

        var command = mapper.Map<UpdateOrderCommand>(request);
        command.Id = id;
        
        var response = await mediator.Send(command, cancellationToken);

        return Ok(mapper.Map<UpdateOrderResponse>(response));
    }
}