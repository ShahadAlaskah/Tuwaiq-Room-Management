using System.Linq.Expressions;
using Shared.Domain;

namespace Application.Interfaces;

public interface IGenericRepositoryCurd<T> : IGenericRepository<T> where T : IBaseEntity
{
    // EntityEntry Entity { get; }
    void Create(T model);
    Task CreateAsync(T model, CancellationToken cancellationToken);
    void Create(List<T> models);
    void Update(T model);
    void Update(ISpecification<T>? specification, Action<T> method);
    void Update(IQueryable<T> query, Action<T> method);
    void Update(Expression<Func<T, bool>> query, Action<T> method);
    void DeleteById(object id);
    void Delete(T item);
    void Delete(ISpecification<T>? specification);
    void Delete(Expression<Func<T, bool>> query);

    void Save();
    Task SaveAsync();
}