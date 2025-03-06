using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public static class UserSpecifications
{
    public static bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }

    public static BaseSpecification<User> ListUsersPaginated(string genericOrder, int page, int size)
    {
        var spec = new BaseSpecification<User>(x => x.Status != UserStatus.Deleted);
        spec.ApplyPaging(page, size);
        spec.ApplyGenericOrder(genericOrder);

        return spec;
    }
}