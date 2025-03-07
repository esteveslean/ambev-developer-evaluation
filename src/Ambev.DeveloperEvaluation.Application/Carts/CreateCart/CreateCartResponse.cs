﻿using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Carts;

public class CreateCartResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CartItemDTO> Products { get; set; } = new();
}