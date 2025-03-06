using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

public class ListUsersProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public ListUsersProfile()
    {
        CreateMap<ListUsersRequest, ListUsersCommand>();
        CreateMap<ListUsersResult, ListUsersResponse>();
        CreateMap<ListUsersResponse, ListUsersResult>();
    }
}