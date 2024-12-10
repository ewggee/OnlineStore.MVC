using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Products;
using OnlineStore.Core.Categories.Services;
using OnlineStore.Core.Common.Extensions;
using OnlineStore.Core.Common.Models;
using OnlineStore.Core.Products.Services;

namespace OnlineStore.MVC.Controllers
{
    [Route("catalog")]
    public class CatalogController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly PaginationOptions _paginationOptions;

        public CatalogController(
            ICategoryService categoryService,
            IProductService productService,
            IOptions<PaginationOptions> paginationOptions)
        {
            _categoryService = categoryService;
            _productService = productService;
            _paginationOptions = paginationOptions.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellation)
        {
            var mainCategories = await _categoryService.GetMainCategoriesAsync(cancellation);

            ViewBag.Title = "Каталог";
            return View("Category", mainCategories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId, CancellationToken cancellation)
        {
            var existingCategory = await _categoryService.GetAsync(categoryId, cancellation);
            if (existingCategory == null)
            {
                return NoContent();
            }

            var subCategories = await _categoryService.GetSubcategoriesByIdAsync(existingCategory.Id, cancellation);
            if (subCategories.Count > 0)
            {
                ViewBag.Title = existingCategory.Name;
                return View("Category", subCategories);
            }

            return RedirectToAction("GetProductsInCategory", new { categoryId, page = 1 });
        }

        [HttpGet("{categoryId}/products/")]
        public async Task<IActionResult> GetProductsInCategory(GetProductsRequest request, CancellationToken cancellation)
        {
            request.PageSize = _paginationOptions.ProductsPageSize;

            var existingCategoryDto = await _categoryService.GetAsync(request.CategoryId, cancellation);
            
            // Проверка, что переданная категория может содержать в себе товары.
            var independentCategories = await _categoryService.GetWithoutSubcategories(cancellation);

            var isWithoutSubcategories = independentCategories
                .Select(c => c.Id)
                .Contains(existingCategoryDto.Id);

            if (existingCategoryDto == null 
                || !isWithoutSubcategories)
            {
                return NoContent();
            }

            var result = await _productService.GetProductsInCategoryByRequestAsync(request, existingCategoryDto, cancellation);

            // ViewBag методов сортировки товаров.
            var sortingMethodsList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Default", Text = "По умолчанию" }
            };
            foreach (ProductsSortEnum method in Enum.GetValues(typeof(ProductsSortEnum)))
            {
                sortingMethodsList.Add(new SelectListItem
                {
                    Value = method.ToString(),
                    Text = EnumExtensions.GetEnumDescription(method)
                });
            }
            ViewBag.SortingMethod = sortingMethodsList;

            return View("ProductsInCategory", result);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProduct(int productId, CancellationToken cancellation)
        {
            var product = await _productService.GetAsync(productId, cancellation);

            return View("Product", product);
        }
    }
}
