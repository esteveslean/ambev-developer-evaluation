using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CreateCartResponse = Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CreateCartResponse;
using CreateCartValidator = Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart.CreateCartValidator;
using DeleteCartValidator = Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart.DeleteCartValidator;
using GetCartResponse = Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart.GetCartResponse;
using GetCartValidator = Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart.GetCartValidator;
using ListCartsResponse = Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts.ListCartsResponse;
using UpdateCartResponse = Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart.UpdateCartResponse;
using UpdateCartValidator = Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart.UpdateCartValidator;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[ApiController]
[Route("api/[controller]")]
public class CartsController(IMediator mediator, IMapper mapper) : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponseError
            {
                Type = "Validation Error",
                Error = "Invalid input data",
                Detail = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty
            });

        var command = mapper.Map<CreateCartCommand>(request);
        var response = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = mapper.Map<CreateCartResponse>(response)
        });
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetCartRequest(id);

        var validator = new GetCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponseError
            {
                Type = "Validation Error",
                Error = "Invalid input data",
                Detail = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty
            });
        
        var command = mapper.Map<GetCartCommand>(request.Id);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(mapper.Map<GetCartResponse>(response));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<ListCartsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListCarts([FromQuery(Name = "_order")] string order = "",
        [FromQuery(Name = "_page")] int page = 1,
        [FromQuery(Name = "_size")] int size = 10, CancellationToken cancellationToken = default)
    {
        var request = new ListCartsRequest
        {
            Page = page,
            Size = size,
            Order = order
        };

        var validator = new ListCartsValidator();
        ValidationResult? validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = mapper.Map<ListCartsCommand>(request);
        var response = await mediator.Send(command, cancellationToken);

        return Ok(new PaginatedResponse<ListCartsResponse>
        {
            Success = true,
            Message = "List carts retrieved successfully",
            CurrentPage = response.CurrentPage,
            TotalCount = response.TotalItems,
            TotalPages = response.TotalPages,
            Data = mapper.Map<List<ListCartsResponse>>(response.Data.ToList())
        });
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCart([FromRoute] Guid id, [FromBody] UpdateCartRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponseError
            {
                Type = "Validation Error",
                Error = "Invalid input data",
                Detail = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty
            });

        var command = mapper.Map<UpdateCartCommand>(request);
        command.Id = id;
        
        var response = await mediator.Send(command, cancellationToken);

        return Ok(mapper.Map<UpdateCartResponse>(response));
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = mapper.Map<DeleteCartCommand>(request.Id);
        await mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Cart deleted successfully"
        });
    }
}