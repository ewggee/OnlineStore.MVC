using OnlineStore.Contracts.Categories;
using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Enums;

namespace OnlineStore.Contracts.Products
{
    public class ProductsListDto : PagedResponse<ShortProductDto>
    {
        public CategoryDto CategoryDto { get; set; }

        public ProductsSortEnum Sorting { get; set; }
    }
}
