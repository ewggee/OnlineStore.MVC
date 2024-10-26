using OnlineStore.Core.Categories.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Categories.Repositories
{
    public sealed class CategoryRepository(
        MutableOnlineStoreDbContext mutableDbContext,
        ReadonlyOnlineStoreDbContext readOnlyDbContext)
        : RepositoryBase<Category>(mutableDbContext, readOnlyDbContext), ICategoryRepository
    { }
}
