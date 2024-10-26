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
        /// Сохраняет изображение в БД.
        /// </summary>
        Task<int> SaveAsync(ProductImage image, CancellationToken cancellation);

        /// <summary>
        /// Возвращает изображение по URL.
        /// </summary>
        Task<ProductImage?> GetByUrlAsync(string url, CancellationToken cancellation);
    }
}
