using AutoMapper;
using OnlineStore.Contracts.Categories;
using OnlineStore.Core.Categories.Repositories;

namespace OnlineStore.Core.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<CategoryDto>> GetCategoriesAsync(CancellationToken cancellation)
        {
            var result = await _repository.GetAllAsync(cancellation);

            return _mapper.Map<IReadOnlyCollection<CategoryDto>>(result);
        }
    }
}
