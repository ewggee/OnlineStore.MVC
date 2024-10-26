using AutoMapper;
using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Products;
using OnlineStore.Core.Products.Repositories;

namespace OnlineStore.Core.Products.Services
{
    /// <summary>
    /// Сервис по работе с товарами.
    /// </summary>
    public sealed class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository, 
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<ShortProductDto> GetProductByIdAsync(int productId, CancellationToken cancellation)
        {
            var product = await _productRepository.GetAsync(productId);

            var dto = _mapper.Map<ShortProductDto>(product);
            return dto;
        }

        /// <inheritdoc/>
        public Task<ProductsListDto> GetProductsAsync(PagedRequest request, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
