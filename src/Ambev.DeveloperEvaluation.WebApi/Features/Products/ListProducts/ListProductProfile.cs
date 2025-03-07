using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductProfile : Profile
{
    public ListProductProfile()
    {
        CreateMap<ListProductRequest, ListProductsCommand>();
        CreateMap<ListProductsResult, ListProductResponse>();
        CreateMap<ListProductResponse, ListProductsResult>();
    }
}