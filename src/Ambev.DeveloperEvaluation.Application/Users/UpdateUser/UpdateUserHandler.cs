using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

public class UpdateUserCommand : IRequest<UpdateUserResult>
{
    /// <summary>
    /// Gets or sets the username of the user to be created.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user to be created.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number for the user.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address for the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    public NameDTO Name { get; set; } = new();
    public AddressDTO Address { get; set; } = new();

    /// <summary>
    /// Gets or sets the status of the user.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public UserRole Role { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateUserCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

public class UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        User? userDb = await userRepository.GetByIdAsync(command.Id, cancellationToken);
        if (userDb == null)
            throw new KeyNotFoundException($"User with id {command.Id} not found");

        userDb.Update(command.Username, command.Phone, command.Email, command.Password, command.Status, command.Role,
            command.Name, command.Address);
        
        User user = await userRepository.UpdateAsync(userDb, cancellationToken);
        
        return mapper.Map<UpdateUserResult>(user);
    }
}

public class UpdateUserResult
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public NameDTO Name { get; set; } = new();
    public AddressDTO Address { get; set; } = new();
    public UserStatus Status { get; set; }
    public UserRole Role { get; set; }
}