using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Represents the response returned after successfully creating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateUserResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created user.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created user in the system.</value>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the username of the user to be created.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    public NameDTO Name { get; set; } = new();
    public AddressDTO Address { get; set; } = new();

    /// <summary>
    /// Gets or sets the phone number for the user.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address for the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status of the user.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
