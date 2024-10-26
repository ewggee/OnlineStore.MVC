using Microsoft.AspNetCore.Http;
using OnlineStore.Contracts.Images;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Images.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с изображениями.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Сохраняет изображение.
        /// </summary>
        Task<string> SaveImageAsync(IFormFile imageFile, CancellationToken cancellation);

        /// <summary>
        /// Возвращает изображение по ID.
        /// </summary>
        Task<ImageDto> GetImageDtoAsync(int id, CancellationToken cancellation);

        /// <summary>
        /// Возвращает URLs изображений из массива.
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        string[] GetImagesUrls(ProductImage[] images);

        /// <summary>
        /// Сохраняет список изображений.
        /// </summary>
        Task<IReadOnlyCollection<string>> SaveImagesAsync(List<IFormFile> ImageFiles, CancellationToken cancellation);

        /// <summary>
        /// Сохраняет изображения товара.
        /// </summary>
        /// <param name="imagesUrls">Массив из URL изображений.</param>
        /// <param name="product">Товар, к которому будут прикреплены изображения.</param>
        Task<ProductImage[]> SaveProductImagesAsync(string[] imagesUrls, Product product, CancellationToken cancellation);
    }
}
