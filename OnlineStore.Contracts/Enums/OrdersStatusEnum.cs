using System.ComponentModel;

namespace OnlineStore.Contracts.Enums
{
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public enum OrdersStatusEnum
    {
        /// <summary>
        /// Принят.
        /// </summary>
        [Description("Принят")]
        Accepted = 1,

        /// <summary>
        /// В обработке.
        /// </summary>
        [Description("В обработке")]
        Processing,

        /// <summary>
        /// В пути.
        /// </summary>
        [Description("В пути")]
        Shipping,

        /// <summary>
        /// Доставлен.
        /// </summary>
        [Description("Доставлен")]
        Delivered,

        /// <summary>
        /// Отменен.
        /// </summary>
        [Description("Отменен")]
        Canceled
    }
}

