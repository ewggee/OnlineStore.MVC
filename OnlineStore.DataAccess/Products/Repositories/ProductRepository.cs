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
        public Task<List<Product>> GetProductsByIdsAsync(int[] ids, CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => ids.Contains(p.Id))
                .Where(p => p.IsDeleted == false)
                .ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<List<Product>> GetProductsAsync(GetProductsRequest request, CancellationToken cancellation)
        {
            var query = ReadOnlyDbContext
                .Set<Product>()
                .Where(p => p.CategoryId == request.CategoryId)
                .Where(p => p.IsDeleted == false);
            
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
                .Where(p => p.CategoryId == categoryId)
                .Where(p => p.IsDeleted == false);

            return query.ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<int> GetProductsTotalCountAsync(int categoryId, CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => p.CategoryId == categoryId)
                .Where(p => p.IsDeleted == false)
                .CountAsync(cancellation);
        }

        /// <inheritdoc/>
        public override Task<Product?> GetAsync(int id)
        {
            return ReadOnlyDbContext
                .Set<Product>()
                .Where(p => p.Id == id)
                .Where(p => p.IsDeleted == false)
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

        /// <inheritdoc/>
        public Task UpdateProductsCountAsync(List<Product> products, CancellationToken cancellation)
        {
            foreach (Product product in products)
            {
                MutableDbContext.Set<Product>().Attach(product);
                MutableDbContext.Entry(product).Property(p => p.StockQuantity).IsModified = true;
            }

            return MutableDbContext.SaveChangesAsync(cancellation);
        }

        /// <inheritdoc/>
        public override Task DeleteAsync(Product product, CancellationToken cancellation)
        {
            MutableDbContext.Set<Product>().Attach(product);
            MutableDbContext.Entry(product).Property(p => p.IsDeleted).IsModified = true;

            return MutableDbContext.SaveChangesAsync(cancellation);
        }
    }
}
