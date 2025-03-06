using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    Expression<Func<T, object>>? GroupBy { get; }
    string? GenericOrder { get; }
    int Page { get; }
    int Size { get; }
    IQueryable<T> GetGenericOrderingQuery(IQueryable<T> query);
}
