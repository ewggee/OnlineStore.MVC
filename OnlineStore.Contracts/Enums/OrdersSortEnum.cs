using System.ComponentModel;

namespace OnlineStore.Contracts.Enums
{
    /// <summary>
    /// Enum метода сортировки заказов.
    /// </summary>
    public enum OrdersSortEnum
    {
        /// <summary>
        /// Сначала старые.
        /// </summary>
        [Description("Сначала старые")]
        CreatedDateAsc = 1,

        /// <summary>
        /// Сначала новые.
        /// </summary>
        [Description("Сначала новые")]
        CreatedDateDesc,

        /// <summary>
        /// По возрастанию общей стоимости.
        /// </summary>
        [Description("По возрастанию общей стоимости")]
        TotalPriceAsc,

        /// <summary>
        /// По убыванию общей стоимости.
        /// </summary>
        [Description("По убыванию общей стоимости")]
        TotalPriceDesc,

        /// <summary>
        /// По возрастанию статуса.
        /// </summary>
        [Description("По возрастанию статуса")]
        StatusAsc,

        /// <summary>
        /// По убыванию статуса.
        /// </summary>
        [Description("По убыванию статуса")]
        StatusDesc 
    }
}
