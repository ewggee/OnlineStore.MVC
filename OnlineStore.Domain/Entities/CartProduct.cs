namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Товар корзины.
    /// </summary>
    public sealed class CartProduct
    {
        public int Id { get; set; }

        /// <summary>
        /// ID продукта.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// ID корзины.
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Корзина.
        /// </summary>
        public Cart Cart { get; set; }

        /// <summary>
        /// Товар.
        /// </summary>
        public Product Product { get; set; }
    }
}
