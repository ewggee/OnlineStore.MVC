using System.ComponentModel;

namespace OnlineStore.Contracts.Enums
{
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public enum OrderStatusEnum
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
        Processing = 2,

        /// <summary>
        /// В пути.
        /// </summary>
        [Description("В пути")]
        Shipping = 3,

        /// <summary>
        /// Доставлен.
        /// </summary>
        [Description("Доставлен")]
        Delivered = 4,

        /// <summary>
        /// Отменен.
        /// </summary>
        [Description("Отменен")]
        Canceled = 5
    }
}

