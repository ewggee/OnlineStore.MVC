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
        public override Task<List<Product>> GetAllAsync(CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Include(x => x.Category)
                .ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<List<Product>> GetProductsByIdsAsync(List<int> ids, CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => ids.Contains(p.Id))
                .ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<List<Product>> GetProductsAsync(GetProductsRequest request, CancellationToken cancellation)
        {
            var query = ReadOnlyDbContext
                .Set<Product>()
                .Where(p => p.CategoryId == request.CategoryId)
                .Where(p => !p.IsDeleted);
            
            query = query
                .OrderBy(p => p.Id)
                .Skip(request.Skip);

            if (request.Take != 0)
            {
                query = query
                    .Take(request.Take);
            }

            return query.ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId, CancellationToken cancellation)
        {
            var query =
                ReadOnlyDbContext
                .Set<Product>()
                .Where(p => p.IsDeleted == false)
                .Where(p => p.CategoryId == categoryId);

            return query.ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<int> GetProductsTotalCountAsync(int categoryId, CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => p.CategoryId == categoryId)
                .Where(p => !p.IsDeleted)
                .CountAsync(cancellation);
        }

        /// <inheritdoc/>
        public override Task<Product> GetAsync(int id)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => p.Id == id)
                .Where(p => !p.IsDeleted)
                .Include(p => p.Images)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public override Task UpdateAsync(Product product, CancellationToken cancellation)
        {
            MutableDbContext.Set<Product>().Attach(product);

            MutableDbContext.Entry(product).State = EntityState.Modified;
            MutableDbContext.Entry(product).Property(p => p.CreatedAt).IsModified = false;

            return MutableDbContext.SaveChangesAsync(cancellation);
        }
    }
}
