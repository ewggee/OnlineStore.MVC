using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Enums;

namespace OnlineStore.Contracts.Products
{
    public sealed class GetProductsRequest : PagedRequest
    {
        /// <summary>
        /// ID категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Тип сортировки.
        /// </summary>
        public ProductsSortEnum Sort { get; set; }
    }
}
