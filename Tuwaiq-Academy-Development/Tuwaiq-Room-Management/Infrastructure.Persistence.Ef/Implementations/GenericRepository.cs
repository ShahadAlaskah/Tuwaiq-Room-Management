using System.Linq.Expressions;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace Infrastructure.Persistence.Ef.Implementations;

public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class, IBaseEntity
{
    protected readonly DbContext Context;
    protected readonly DbSet<T>? DbSet;

    private bool _disposed;

    public GenericRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task<IQueryable<T>> GetAllAsync(bool noTracking = true)
    {
        if (noTracking)
            return Task.FromResult(DbSet!.AsQueryable().AsNoTracking());
        return Task.FromResult(DbSet!.AsQueryable());
    }

    public IQueryable<T> GetAll(bool noTracking = true)
    {
        if (noTracking)
            return DbSet!.AsQueryable().AsNoTracking();
        return DbSet!.AsQueryable();
    }


    public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> query, bool noTracking = true)
    {
        if (noTracking)
            return Task.FromResult(DbSet!.AsQueryable().Where(query).AsNoTracking());
        return Task.FromResult(DbSet!.AsQueryable().Where(query));
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> query, bool noTracking = true)
    {
        if (noTracking)
            return DbSet!.AsQueryable().Where(query).AsNoTracking();
        return DbSet!.AsQueryable().Where(query);
    }

    public async Task<T?> GetByIdAsync(object? id)
    {
        return (await DbSet!.FindAsync(id))!;
    }

    public IQueryable<T> FindWithSpecificationPattern(ISpecification<T>? specification = null)
    {
        return SpecificationEvaluator<T>.GetQuery(DbSet!.AsQueryable(), specification!);
    }

    public IQueryable<T> FindWithSpecificationPattern(IList<T> query, ISpecification<T>? specification = null)
    {
        return SpecificationEvaluator<T>.GetQuery(query, specification!);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                Context.Dispose();

        _disposed = true;
    }
}