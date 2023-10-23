using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Ef.Implementations;

public class SpecificationEvaluator<TEntity> where TEntity : class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;
        if (spec.Criteria != null) query = query.Where(spec.Criteria);
        if (spec.OrderBy != null) query = query.OrderBy(spec.OrderBy);
        if (spec.OrderByDescending != null) query = query.OrderByDescending(spec.OrderByDescending);
        //query = spec.Includes.Aggregate(query, (current, include) => include(current));
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }

    public static IQueryable<TEntity> GetQuery(IList<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery.AsQueryable();
        if (spec.Criteria != null) query = query.Where(spec.Criteria);
        return query;
    }
}