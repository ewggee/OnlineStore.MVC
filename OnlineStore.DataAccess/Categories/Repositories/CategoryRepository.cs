using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Categories.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Categories.Repositories
{
    public sealed class CategoryRepository(
        MutableOnlineStoreDbContext mutableDbContext,
        ReadonlyOnlineStoreDbContext readOnlyDbContext)
        : RepositoryBase<Category>(mutableDbContext, readOnlyDbContext), ICategoryRepository
    {
        public Task<List<Category>> GetMainAsync (CancellationToken cancellation)
        {
            var query =
                ReadOnlyDbContext
                .Set<Category>()
                .Where(c => c.ParentCategoryId == null);

            return query.ToListAsync(cancellation);
        }

        public Task<List<Category>> GetSubsAsync(int categoryId, CancellationToken cancellation)
        {
            var query =
                ReadOnlyDbContext
                .Set<Category>()
                .Where(c => c.ParentCategoryId == categoryId);

            return query.ToListAsync(cancellation);
        }

        public Task<List<Category>> GetNavigationCategoriesAsync(CancellationToken cancellation, int? categoryId)
        {
            var query =
                ReadOnlyDbContext
                .Set<Category>()
                .Where(c => c.Products.Count == 0);

            if (categoryId != null)
            {
                query = query
                    .Where(c => c.Id != categoryId);

                var dependentCategoriesIds = GetSubcategoryiesIds(categoryId.Value).ToList();

                query = query
                    .Where(c => !dependentCategoriesIds.Contains(c.Id));
            }

            return query.ToListAsync(cancellation);
        }

        public Task<List<Category>> GetWithoutSubs(CancellationToken cancellation)
        {
            var categories =
                ReadOnlyDbContext
                .Set<Category>()
                .AsQueryable();

            var query =
                categories
                .Where(c => !categories
                    .Any(subc => subc.ParentCategoryId == c.Id)
                );

            return query.ToListAsync(cancellation);
        }

        private IEnumerable<int> GetSubcategoryiesIds(int categoryId)
        {
            var subcategoriesIds =
                ReadOnlyDbContext
                .Set<Category>()
                .Where(c => c.ParentCategoryId == categoryId)
                .Select(subc => subc.Id)
                .ToList();

            return subcategoriesIds
                    .Union(
                        subcategoriesIds
                        .SelectMany(GetSubcategoryiesIds));
        }
    }
}
