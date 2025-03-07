using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public static class OrderSpecifications
{
    public static BaseSpecification<Order> ListOrdersPaginated(string genericOrder, int page, int size)
    {
        var spec = new BaseSpecification<Order>();
        spec.ApplyPaging(page, size);
        spec.ApplyGenericOrder(genericOrder);

        return spec;
    }
}