using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public static class ProductSpecifications
{
    public static BaseSpecification<Product> ListProductsPaginated(string genericOrder, int page, int size)
    {
        var spec = new BaseSpecification<Product>();
        spec.ApplyPaging(page, size);
        spec.ApplyGenericOrder(genericOrder);

        return spec;
    }
}