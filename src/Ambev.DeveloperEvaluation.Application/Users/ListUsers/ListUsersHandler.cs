using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersCommand : IRequest<PaginatedList<ListUsersResponse>>
{
    public string Order { get; set; } = string.Empty;
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 1;
}

public class ListUsersHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<ListUsersCommand, PaginatedList<ListUsersResponse>>
{
    public async Task<PaginatedList<ListUsersResponse>> Handle(ListUsersCommand request, CancellationToken cancellationToken)
    {
        BaseSpecification<User> spec = UserSpecifications.ListUsersPaginated(request.Order, request.Page, request.Size);
        
        List<User> users = await userRepository.ListAsync(spec, cancellationToken);
        
        return new PaginatedList<ListUsersResponse>(mapper.Map<List<ListUsersResponse>>(users), users.Count, spec.Page, spec.Size);
    }
}

public class ListUsersResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public NameDTO Name { get; set; } = new ();
    public AddressDTO Address { get; set; } = new();
    public UserStatus Status { get; set; }
    public UserRole Role { get; set; }
}