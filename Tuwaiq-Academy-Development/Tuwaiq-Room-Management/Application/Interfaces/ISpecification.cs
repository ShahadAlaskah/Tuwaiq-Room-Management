using System.Linq.Expressions;

namespace Application.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }

    List<Expression<Func<T, object>>> Includes { get; }

    List<string> IncludeStrings { get; }
    //List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }

    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
}