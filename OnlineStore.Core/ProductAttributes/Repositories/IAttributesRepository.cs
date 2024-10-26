using OnlineStore.Core.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.ProductAttributes.Repositories
{
    /// <summary>
    /// Интерфейс репозитория по работе с атрибутами.
    /// </summary>
    public interface IAttributesRepository : IRepository<ProductAttribute>
    { }
}
