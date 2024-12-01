using OnlineStore.Contracts.Categories;

namespace OnlineStore.MVC.Models
{
    public class CategoryWithSubcategoriesViewModel
    {
        public CategoryDto? CategoryDto { get; set; }

        public List<CategoryDto> Subcategories { get; set; } = [];
    }
}
