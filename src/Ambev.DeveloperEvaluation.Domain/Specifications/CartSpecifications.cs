using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public static class CartSpecifications
{
    public static BaseSpecification<Cart> ListCartsPaginated(string genericOrder, int page, int size)
    {
        var spec = new BaseSpecification<Cart>();
        spec.ApplyPaging(page, size);
        spec.ApplyGenericOrder(genericOrder);

        return spec;
    }
}