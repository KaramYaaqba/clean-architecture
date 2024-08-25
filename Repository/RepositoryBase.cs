using System;
using System.Linq.Expressions;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected CompanyDbContext CompanyDbContext;
    public RepositoryBase(CompanyDbContext companyDbContext)
    => CompanyDbContext = companyDbContext;
    public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? CompanyDbContext.Set<T>().AsNoTracking() : CompanyDbContext.Set<T>();
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        !trackChanges ? CompanyDbContext.Set<T>().Where(expression).AsNoTracking() : CompanyDbContext.Set<T>().Where(expression);
    public void Create(T entity) => CompanyDbContext.Set<T>().Add(entity);
    public void Update(T entity) => CompanyDbContext.Set<T>().Update(entity);
    public void Delete(T entity) => CompanyDbContext.Set<T>().Remove(entity);
}