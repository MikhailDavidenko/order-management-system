using System.Linq.Expressions;

namespace OrderManagementSystem.Application.Common;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
}