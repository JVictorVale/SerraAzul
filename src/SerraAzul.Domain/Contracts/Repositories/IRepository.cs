using System.Linq.Expressions;
using SerraAzul.Domain.Contracts.Interfaces;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Domain.Contracts.Repositories;

public interface IRepository<T> : IDisposable where T : BaseEntity, IAggragateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression);
    Task<bool> Any(Expression<Func<T, bool>> expression);
}