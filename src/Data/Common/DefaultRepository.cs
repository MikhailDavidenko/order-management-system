using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Application.Common;

namespace OrderManagementSystem.Data.Common;

internal class DefaultRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly AppDbContext context;

    protected readonly DbSet<TEntity> DbSet;

    public DefaultRepository(
        AppDbContext context)
    {
        this.context = context;
        DbSet = context.Set<TEntity>();
    }

    public Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return DbSet
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
}