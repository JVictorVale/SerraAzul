using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SerraAzul.Domain.Contracts.Interfaces;
using SerraAzul.Domain.Contracts.Repositories;
using SerraAzul.Domain.Entities;
using SerraAzul.Infra.Data.Context;

namespace SerraAzul.Infra.Data.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity, IAggragateRoot
{
    protected readonly SerraAzulDbContext Context;
    private readonly DbSet<T> _dbSet;
    private bool _isDisposed;

    protected Repository(SerraAzulDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public IUnitOfWork UnitOfWork => Context;
    
    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(expression);
    }

    public async Task<bool> Any(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            Context.Dispose();
        }

        _isDisposed = true;
    }
    
    ~Repository()
    {
        Dispose(false);
    }
}