using OnlineStore.Core.ProductAttributes.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.ProductAttributes.Repositories
{
    /// <summary>
    /// Репозиторий по работе с атрибутами.
    /// </summary>
    public sealed class AttributesRepository(
        MutableOnlineStoreDbContext mutableDbContext,
        ReadonlyOnlineStoreDbContext readOnlyDbContext) 
        : RepositoryBase<ProductAttribute>(mutableDbContext, readOnlyDbContext), IAttributesRepository
    { }
}
