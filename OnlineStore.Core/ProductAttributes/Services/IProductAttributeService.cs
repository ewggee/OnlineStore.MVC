using OnlineStore.Contracts.ProductAttributes;

namespace OnlineStore.Core.ProductAttributes.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с атрибутами продукта.
    /// </summary>
    public interface IProductAttributeService
    {
        /// <summary>
        /// Возвращает атрибут продукта по ID.
        /// </summary>
        Task<ProductAttributeDto> GetAsync(int id);
    }
}
