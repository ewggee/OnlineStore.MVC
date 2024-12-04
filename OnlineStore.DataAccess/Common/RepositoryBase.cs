using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Common;

namespace OnlineStore.DataAccess.Common
{
    /// <summary>
    /// Базовый репозиторий для работы с БД через EF.
    /// </summary>
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        public readonly MutableOnlineStoreDbContext MutableDbContext;
        public readonly ReadonlyOnlineStoreDbContext ReadOnlyDbContext;

        /// <inheritdoc/>
        public RepositoryBase(
            MutableOnlineStoreDbContext mutableDbContext,
            ReadonlyOnlineStoreDbContext readOnlyDbContext)
        {
            MutableDbContext = mutableDbContext;
            ReadOnlyDbContext = readOnlyDbContext;
        }

        /// <inheritdoc/>
        public virtual Task<T?> GetAsync(int id)
        {
            return MutableDbContext.FindAsync<T>(id).AsTask();
        }

        /// <inheritdoc/>
        public virtual Task<List<T>> GetAllAsync(CancellationToken cancellation)
        {
            return ReadOnlyDbContext.Set<T>().ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public async Task AddAsync(T entity, CancellationToken cancellation)
        {
            await MutableDbContext.AddAsync(entity, cancellation);
            await MutableDbContext.SaveChangesAsync(cancellation);
        }

        /// <inheritdoc/>
        public virtual Task UpdateAsync(T entity, CancellationToken cancellation)
        {
            MutableDbContext.Set<T>().Attach(entity);
            MutableDbContext.Entry(entity).State = EntityState.Modified;

            return MutableDbContext.SaveChangesAsync(cancellation);
        }

        /// <inheritdoc/>
        public virtual Task DeleteAsync(T entity, CancellationToken cancellation)
        {
            MutableDbContext.Remove(entity);
            return MutableDbContext.SaveChangesAsync(cancellation);
        }
    }
}
