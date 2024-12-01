using OnlineStore.Contracts.Categories;
using OnlineStore.Contracts.Common;

namespace OnlineStore.Contracts.Products
{
    public class ProductsListDto : PagedResponse<ShortProductDto>
    {
        public CategoryDto CategoryDto { get; set; }
    }
}
