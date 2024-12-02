namespace OnlineStore.Contracts.Orders
{
    public sealed class OrderItemDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Название товара.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Количество товара.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена за шт.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
