using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Products.Models;
using OnlineStore.Core.Products.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Products.Repositories
{
    /// <summary>
    /// Репозиторий по работе с товарами.
    /// </summary>
    public sealed class ProductRepository(
        MutableOnlineStoreDbContext mutableDbContext,
        ReadonlyOnlineStoreDbContext readOnlyDbContext) 
        : RepositoryBase<Product>(mutableDbContext, readOnlyDbContext), IProductRepository
    {
        /// <inheritdoc/>
        public async override Task<List<Product>> GetAllAsync(CancellationToken cancellation)
        {
            return await ReadOnlyDbContext.Set<Product>()
                .Include(x => x.Category)
                .ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<List<Product>> GetProductsAsync(GetProductsRequest request, CancellationToken cancellation)
        {
            var query = ReadOnlyDbContext
                .Set<Product>()
                .AsQueryable()
                .Where(p => !p.IsDeleted);

            if (request.IncludeCategory)
            {
                query = query
                    .Include(p => p.Category);
            }

            if (request.IncludeImages)
            {
                query = query
                    .Include(p => p.Images);
            }

            query = query
                .OrderBy(p => p.Id)
                .Skip(request.Skip);

            if (request.Take != default)
            {
                query = query.Take(request.Take);
            }

            return query.ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<int> GetProductsTotalCountAsync(CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => !p.IsDeleted)
                .CountAsync(cancellation);
        }

        /// <inheritdoc/>
        public override Task<Product> GetAsync(int id)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => (p.Id == id) && (!p.IsDeleted))
                .Include(x => x.Images)
                .FirstOrDefaultAsync();
        }
    }
}
