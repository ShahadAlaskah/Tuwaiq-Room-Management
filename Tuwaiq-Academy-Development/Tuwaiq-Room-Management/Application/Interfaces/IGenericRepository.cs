using System.Linq.Expressions;
using Shared.Domain;

namespace Application.Interfaces;

public interface IGenericRepository<T> where T : IBaseEntity
{
    Task<T?> GetByIdAsync(object? id);
    IQueryable<T> GetAll(bool noTracking = true);
    Task<IQueryable<T>> GetAllAsync(bool noTracking = true);
    IQueryable<T> GetAll(Expression<Func<T, bool>> query, bool noTracking = true);
    Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> query, bool noTracking = true);
    IQueryable<T> FindWithSpecificationPattern(ISpecification<T>? specification = null);
    IQueryable<T> FindWithSpecificationPattern(IList<T> query, ISpecification<T>? specification = null);
}