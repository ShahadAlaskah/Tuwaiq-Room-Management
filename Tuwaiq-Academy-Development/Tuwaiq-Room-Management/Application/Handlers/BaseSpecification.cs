using System.Linq.Expressions;
using Application.Interfaces;

namespace Application.Handlers;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; } = null!;

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public List<string> IncludeStrings { get; } = new();
    //public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; } = new();

    public Expression<Func<T, object>> OrderBy { get; set; } = null!;
    public Expression<Func<T, object>> OrderByDescending { get; set; } = null!;

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddInclude(string include)
    {
        IncludeStrings.Add(include);
    }

    //protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
    //{
    //    Includes.Add(includeExpression);
    //}

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }
}