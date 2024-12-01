namespace OnlineStore.Core.Products.Models
{
    /// <summary>
    /// Запрос на получение товаров.
    /// </summary>
    public sealed class GetProductsRequest
    {
        /// <summary>
        /// Количество выборки.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Пропуск.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// ID категории.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
