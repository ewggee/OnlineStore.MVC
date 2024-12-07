﻿using System.ComponentModel;

namespace OnlineStore.Contracts.Enums
{
    /// <summary>
    /// Enum метода сортировки товаров.
    /// </summary>
    public enum ProductsSortEnum
    {
        /// <summary>
        /// По убыванию цены.
        /// </summary>
        [Description("По убыванию цены")]
        PriceDesc = 1,
        /// <summary>
        /// По возрастанию цены.
        /// </summary>
        [Description("По возрастанию цены")]
        PriceAsc = 2,
        /// <summary>
        /// Сначала новые.
        /// </summary>
        [Description("Сначала новые")]
        NoveltyDesc = 3,
        /// <summary>
        /// Сначала старые.
        /// </summary>
        [Description("Сначала старые")]
        NoveltyAsc = 4
    }
}
