namespace OnlineStore.Contracts.Orders
{
    public class OrderDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string UserEmail { get; set; } = default!;

        /// <summary>
        /// Дата заказа.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Общая сумма заказа.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Идентификатор статуса заказа.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Наименование статуса заказа.
        /// </summary>
        public string OrderStatusName { get; set; } = default!;

        /// <summary>
        /// Позиции заказа.
        /// </summary>
        public OrderItemDto[] Items { get; set; } = [];
    }
}
