using OnlineStore.Core.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Images.Repositories
{
    /// <summary>
    /// Интерфейс репозитория изображений.
    /// </summary>
    public interface IImageRepository : IRepository<ProductImage>
    {
        /// <summary>
        /// Сохраняет изображение в БД и возвращает ID для формирования URL.
        /// </summary>
        /// <returns>ID изображения.</returns>
        Task<int> SaveAsync(ProductImage image, CancellationToken cancellation);
    }
}
