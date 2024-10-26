using AutoMapper;
using OnlineStore.Contracts.ProductAttributes;
using OnlineStore.Core.Common.Cache;
using OnlineStore.Core.ProductAttributes.Repositories;

namespace OnlineStore.Core.ProductAttributes.Services
{
    /// <summary>
    /// Сервис по работе с атрибутами товара.
    /// </summary>
    public sealed class ProductAttributeService : IProductAttributeService
    {
        private readonly IAttributesRepository _attributesRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        /// <inheritdoc/>
        public ProductAttributeService(
            IAttributesRepository attributesRepository,
            IMapper mapper,
            ICacheService cacheService)
        {
            _attributesRepository = attributesRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        /// <inheritdoc/>
        public async Task<ProductAttributeDto> GetAsync(int id)
        {
            var attribute = await _attributesRepository.GetAsync(id);

            var dto = _mapper.Map<ProductAttributeDto>(attribute);

            return dto;
        }
    }
}
