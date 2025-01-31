using System.Linq.Expressions;

namespace OrderManagementSystem.Application.Common;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<IReadOnlyList<TEntity>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken);

    void Update(TEntity entity);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void Remove(TEntity entity);
}