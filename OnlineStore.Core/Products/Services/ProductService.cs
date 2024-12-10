using AutoMapper;
using Microsoft.Extensions.Options;
using OnlineStore.Contracts.Categories;
using OnlineStore.Contracts.Products;
using OnlineStore.Core.Common.DateTimeProviders;
using OnlineStore.Core.Images;
using OnlineStore.Core.Images.Services;
using OnlineStore.Core.Products.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Products.Services
{
    /// <summary>
    /// Сервис по работе с товарами.
    /// </summary>
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;
        private readonly ImageOptions _imageOptions;

        public ProductService(
            IProductRepository productRepository,
            IImageService imageService,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper,
            IOptions<ImageOptions> imageOptions)
        {
            _productRepository = productRepository;
            _imageService = imageService;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
            _imageOptions = imageOptions.Value;
        }

        /// <inheritdoc/>
        public async Task<ShortProductDto> GetAsync(int productId, CancellationToken cancellation)
        {
            var existingProduct = await _productRepository.GetAsync(productId);
            var productDto = _mapper.Map<ShortProductDto>(existingProduct);

            if (existingProduct != null)
            {
                // Загрузка изображений
                var imagesUrls = new List<string>();
                var mainImageId = _imageService.ExtractImageId(productDto.MainImageUrl);
                foreach (var image in existingProduct.Images)
                {
                    if (image.Id != mainImageId)
                    {
                        imagesUrls.Add(string.Concat(_imageOptions.ImagesUrl, image.Id));
                    }
                }
                productDto.ImagesUrls = imagesUrls.ToArray();
            }

            return productDto;
        }

        /// <inheritdoc/>
        public async Task<ProductsListDto> GetProductsInCategoryByRequestAsync(GetProductsRequest request, CategoryDto categoryDto, CancellationToken cancellation)
        {
            var totalCount = await _productRepository.GetProductsTotalCountAsync(request.CategoryId, cancellation);

            if (totalCount == 0)
            {
                return new ProductsListDto
                {
                    Page = 1,
                    TotalCount = totalCount,
                    PageSize = 1,
                    Result = [],
                    CategoryDto = categoryDto,
                    Sorting = request.Sort
                };
            }

            var products = await _productRepository.GetProductsAsync(request, cancellation);

            var productsDtos = _mapper.Map<List<ShortProductDto>>(products);

            return new ProductsListDto
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                Result = productsDtos,
                CategoryDto = categoryDto,
                Sorting = request.Sort
            };
        }

        /// <inheritdoc/>
        public async Task<List<ShortProductDto>> GetAllProductsInCategoryByIdAsync(int categoryId, CancellationToken cancellation)
        {
            var productsInCategory = await _productRepository.GetProductsByCategoryIdAsync(categoryId, cancellation);

            return _mapper.Map<List<ShortProductDto>>(productsInCategory);
        }

        /// <inheritdoc/>
        public async Task<List<ShortProductDto>> GetProductsByIdsAsync(int[] productsIds, CancellationToken cancellation)
        {
            var products = await _productRepository.GetProductsByIdsAsync(productsIds, cancellation);

            return _mapper.Map<List<ShortProductDto>>(products);
        }

        /// <inheritdoc/>
        public async Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellation)
        {
            var product = _mapper.Map<Product>(productDto);

            product.Images = await _imageService.SaveProductImagesAsync(productDto.ImagesUrls, product, cancellation);
            product.CreatedAt = _dateTimeProvider.UtcNow;

            await _productRepository.AddAsync(product, cancellation);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(ShortProductDto productDto, CancellationToken cancellation)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Images = await _imageService.SaveProductImagesAsync(productDto.ImagesUrls, product, cancellation);
            product.UpdatedAt = _dateTimeProvider.UtcNow;
            
            await _productRepository.UpdateAsync(product, cancellation);
        }

        /// <inheritdoc/>
        public async Task UpdateProductsCountAsync(List<ShortProductDto> productDtos, CancellationToken cancellation)
        {
            var products = _mapper.Map<List<Product>>(productDtos);

            await _productRepository.UpdateProductsCountAsync(products, cancellation);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int productId, CancellationToken cancellation)
        {
            var product = await _productRepository.GetAsync(productId);
            product!.IsDeleted = true;

            await _productRepository.DeleteAsync(product, cancellation);
        }
    }
}
