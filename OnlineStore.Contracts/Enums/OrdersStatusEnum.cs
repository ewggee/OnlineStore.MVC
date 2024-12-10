using System.ComponentModel;

namespace OnlineStore.Contracts.Enums
{
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public enum OrdersStatusEnum
    {
        /// <summary>
        /// В обработке.
        /// </summary>
        [Description("В обработке")]
        Processing = 1,

        /// <summary>
        /// Отменен.
        /// </summary>
        [Description("Отменен")]
        Canceled,

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
        /// Получен.
        /// </summary>
        [Description("Получен")]
        Accepted
    }
}

