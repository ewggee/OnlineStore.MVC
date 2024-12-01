using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineStore.Contracts.Common;
using OnlineStore.Core.Categories.Services;
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

        [HttpGet]
        [Route("{categoryId}")]
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

        [HttpGet]
        [Route("{categoryId}/products/")]
        public async Task<IActionResult> GetProductsInCategory(int categoryId, CancellationToken cancellation, int page = 1)
        {
            var existingCategoryDto = await _categoryService.GetAsync(categoryId, cancellation);
            
            // Проверка, что переданная категория имеет возможность содержать в себе товары.
            var independentCategories = await _categoryService.GetWithoutSubcategories(cancellation);

            var isWithoutSubcategories = independentCategories
                .Select(c => c.Id)
                .Contains(existingCategoryDto.Id);

            if (existingCategoryDto == null 
                || !isWithoutSubcategories)
            {
                return NoContent();
            }

            var result = await _productService.GetProductsInCategoryByRequestAsync(new PagedRequest
            {
                PageNumber = page,
                PageSize = _paginationOptions.PageSize
            }, existingCategoryDto, cancellation);

            return View("ProductsInCategory", result);
        }

        [HttpGet]
        [Route("product/{productId}")]
        public async Task<IActionResult> GetProduct(int productId, CancellationToken cancellation)
        {
            var product = await _productService.GetProductByIdAsync(productId, cancellation);

            return View("Product", product);
        }
    }
}
