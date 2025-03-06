namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class UserEmailAlreadyExistsException(string email) : Exception($"User with email {email} already exists");
