using System.Linq.Expressions;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria = null) : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; } = criteria;

    public string? GenericOrder { get; private set; }
    
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public List<string> IncludeStrings { get; } = [];
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public Expression<Func<T, object>>? GroupBy { get; private set; }
    public int Page { get; private set; } = 1;
    public int Size { get; private set; } = 10;

    public virtual void ApplyPaging(int page, int size)
    {
        Page = page < 1 ? 1 : page;
        Size = size < 1 ? 10 : size;
    }
    
    public virtual void ApplyGenericOrder(string? genericOrder)
    {
        GenericOrder = genericOrder;
    }
    
    public virtual IQueryable<T> GetGenericOrderingQuery(IQueryable<T> query)
    {
        if (string.IsNullOrEmpty(GenericOrder))
            return query;
        
        foreach (string filter in GenericOrder.Split(','))
        {
            string[] filterList = filter.Trim().Split(' ');

            if (filterList.Length != 2)
                throw new InvalidOperationException("Filter parameters must contain exactly 2 values'");

            ParameterExpression parameter = Expression.Parameter(typeof(T), "u");
            MemberExpression property = Expression.Property(parameter, filterList.First());

            MethodInfo? orderMethod = typeof(Queryable).GetMethods()
                .FirstOrDefault(m => m.Name == (filterList.Last().Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "OrderByDescending" : "OrderBy") && m.GetParameters().Length == 2)
                ?.MakeGenericMethod(typeof(T), property.Type);

            if (orderMethod == null)
                continue;
            
            query = (IQueryable<T>)orderMethod.Invoke(null, new object[] { query, Expression.Lambda(property, parameter) });
        }

        return query;
    }
}