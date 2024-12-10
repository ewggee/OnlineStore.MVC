using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OnlineStore.Contracts.ApplicationRoles;
using OnlineStore.Contracts.Categories;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Products;
using OnlineStore.Core.Categories.Services;
using OnlineStore.Core.Common.Extensions;
using OnlineStore.Core.Common.Models;
using OnlineStore.Core.Products.Services;
using OnlineStore.MVC.Models;

namespace OnlineStore.MVC.Controllers
{
    [Authorize(Roles = AppRoles.ADMIN)]
    [Route("admin/catalog")]
    public class AdminCatalogController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly PaginationOptions _paginationOptions;

        public AdminCatalogController(
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

            var viewModel = new CategoryWithSubcategoriesViewModel
            {
                CategoryDto = null,
                Subcategories = mainCategories
            };

            ViewBag.Title = "Каталог";
            return View("Categories", viewModel);
        }

        #region CRUD for Category

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId, CancellationToken cancellation)
        {
            var existingCategoryDto = await _categoryService.GetAsync(categoryId, cancellation);
            if (existingCategoryDto == null)
            {
                return NoContent();
            }

            var subCategories = await _categoryService.GetSubcategoriesByIdAsync(existingCategoryDto.Id, cancellation);
            if (subCategories.Count > 0)
            {
                var viewModel = new CategoryWithSubcategoriesViewModel
                {
                    CategoryDto = existingCategoryDto,
                    Subcategories = subCategories
                };

                ViewBag.Title = existingCategoryDto.Name;
                return View("Categories", viewModel);
            }

            return RedirectToAction("GetProductsInCategory", new { categoryId, page = 1 });
        }

        [HttpGet("category/add")]
        public async Task<IActionResult> AddCategory(int parentCategoryId, CancellationToken cancellation)
        {
            var navigationCategories = await _categoryService.GetNavigationCategoriesAsync(cancellation);

            ViewBag.Categories = new SelectList(navigationCategories, dataValueField: "Id", dataTextField: "Name");
            return View();
        }

        [HttpPost("category/add")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto, CancellationToken cancellation)
        {
            await _categoryService.AddAsync(categoryDto, cancellation);

            return RedirectToAction("Index");
        }

        [HttpGet("category/update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, CancellationToken cancellation)
        {
            var existingCategoryDto = await _categoryService.GetAsync(categoryId, cancellation);
            if (existingCategoryDto == null)
            {
                return NoContent();
            }

            var independentCategories = await _categoryService.GetNavigationCategoriesAsync(cancellation, categoryId);
            var products = await _productService.GetAllProductsInCategoryByIdAsync(categoryId, cancellation);

            ViewBag.Categories = new SelectList(independentCategories, dataValueField: "Id", dataTextField: "Name");
            ViewBag.Products = products;

            var subcategories = await _categoryService.GetSubcategoriesByIdAsync(categoryId, cancellation);

            var viewModel = new CategoryWithSubcategoriesViewModel
            {
                CategoryDto = existingCategoryDto,
                Subcategories = subcategories
            };

            return View(viewModel);
        }

        [HttpPost("category/update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto, CancellationToken cancellation)
        {
            await _categoryService.UpdateAsync(categoryDto, cancellation);

            return RedirectToAction("Index");
        }

        [HttpPost("category/delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId, CancellationToken cancellation)
        {
            await _categoryService.DeleteAsync(categoryId, cancellation);

            return RedirectToAction("Index");
        }
        #endregion

        #region CRUD for Product

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
            if (product == null)
            {
                return NoContent();
            }

            return View("Product", product);
        }

        [HttpGet("products/add")]
        public async Task<IActionResult> AddProduct(int categoryId, CancellationToken cancellation)
        {
            var categories = await _categoryService.GetWithoutSubcategories(cancellation);

            ViewBag.Categories = new SelectList(categories, "Id", "Name", categoryId);
            return View();
        }

        [HttpPost("products/add")]
        public async Task<IActionResult> AddProduct(ShortProductDto shortProductDto, CancellationToken cancellation)
        {
            await _productService.AddProductAsync(shortProductDto, cancellation);

            return RedirectToAction("GetProductsInCategory", new { categoryId = shortProductDto.CategoryId });
        }

        [HttpGet("product/update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, CancellationToken cancellation)
        {
            var product = await _productService.GetAsync(productId, cancellation);
            if (product == null)
            {
                return NoContent();
            }

            var categories = await _categoryService.GetWithoutSubcategories(cancellation);

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        [HttpPost("product/update/{productId}")]
        public async Task<IActionResult> UpdateProduct(ShortProductDto shortProductDto, CancellationToken cancellation)
        {
            await _productService.UpdateAsync(shortProductDto, cancellation);

            return RedirectToAction("GetProductsInCategory", new { categoryId = shortProductDto.CategoryId });
        }

        [HttpPost("product/delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId, CancellationToken cancellation)
        {
            var product = await _productService.GetAsync(productId, cancellation);

            if (product == null)
            {
                return NoContent();
            }

            await _productService.DeleteAsync(productId, cancellation);

            return RedirectToAction("GetProductsInCategory", new { categoryId = product.CategoryId });
        }
        #endregion
    }
}
