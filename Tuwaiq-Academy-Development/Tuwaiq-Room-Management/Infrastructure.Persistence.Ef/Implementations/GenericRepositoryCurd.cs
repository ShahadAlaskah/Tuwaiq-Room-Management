using System.Linq.Expressions;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace Infrastructure.Persistence.Ef.Implementations;

public class GenericRepositoryCurd<T> : GenericRepository<T>, IGenericRepositoryCurd<T> where T : class, IBaseEntity
{
    public GenericRepositoryCurd(DbContext context) : base(context)
    {
    }

    public void Create(T model)
    {
        DbSet?.Add(model);
    }

    public async Task CreateAsync(T model, CancellationToken cancellationToken)
    {
        await DbSet!.AddAsync(model, cancellationToken);
    }

    public void Create(List<T> models)
    {
        DbSet?.AddRangeAsync(models);
    }

    public void Update(T model)
    {
        DbSet?.Attach(model);
        Context.Entry(model).State = EntityState.Modified;
    }

    public void Update(ISpecification<T>? specification, Action<T> method)
    {
        var query = SpecificationEvaluator<T>.GetQuery(DbSet!.AsQueryable(), specification!);
        Update(query, method);
    }

    public void Update(IQueryable<T> query, Action<T> method)
    {
        foreach (var item in query) method(item);
    }

    public void Update(Expression<Func<T, bool>> query, Action<T> method)
    {
        var q = DbSet!.AsQueryable().Where(query);
        Update(q, method);
    }

    public void DeleteById(object id)
    {
        if (id == null) throw new Exception("InvalidId");

        var existing = DbSet?.Find(id)!;
        if (existing == null) throw new Exception("NotFound");
        Delete(existing);
    }

    public void Delete(T item)
    {
        if (item == null) throw new Exception("NotFound");
        DbSet?.Remove(item);
    }

    public void Delete(ISpecification<T>? specification)
    {
        var query = SpecificationEvaluator<T>.GetQuery(DbSet!.AsQueryable(), specification!);
        foreach (var item in query) Delete(item);
    }

    public void Delete(Expression<Func<T, bool>> query)
    {
        var q = DbSet!.AsQueryable().Where(query);
        foreach (var item in q) Delete(item);
    }

    public void Save()
    {
        Context.SaveChanges();
    }

    public Task SaveAsync()
    {
        return Context.SaveChangesAsync();
    }
}