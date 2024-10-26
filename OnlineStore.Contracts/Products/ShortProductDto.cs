namespace OnlineStore.Contracts.Products
{
    /// <summary>
    /// Краткая информация о товаре.
    /// </summary>
    public sealed class ShortProductDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Имеющееся количество.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// ID категории.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Главное изображение.
        /// </summary>
        public string MainImageUrl { get; set; }

        /// <summary>
        /// Список изображений.
        /// </summary>
        public string[] ImagesUrls { get; set; } = [];
    }
}
