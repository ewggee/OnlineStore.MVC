using AutoMapper;
using OnlineStore.Contracts.Categories;
using OnlineStore.Core.Categories.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<CategoryDto> GetAsync(int categoryId, CancellationToken cancellation)
        {
            var categoryDto = await _categoryRepository.GetAsync(categoryId);

            return _mapper.Map<CategoryDto>(categoryDto);
        }

        /// <inheritdoc/>
        public async Task<List<CategoryDto>> GetMainCategoriesAsync(CancellationToken cancellation)
        {
            var mainCategories = await _categoryRepository.GetMainAsync(cancellation);

            return _mapper.Map<List<CategoryDto>>(mainCategories);
        }

        /// <inheritdoc/>
        public async Task<List<CategoryDto>> GetSubcategoriesByIdAsync(int categoryId, CancellationToken cancellation)
        {
            var subCategories = await _categoryRepository.GetSubsAsync(categoryId, cancellation);

            return _mapper.Map<List<CategoryDto>>(subCategories);
        }

        /// <inheritdoc/>
        public async Task<List<CategoryDto>> GetNavigationCategoriesAsync(CancellationToken cancellation, int? categoryId = null)
        {
            var navigationCategories = await _categoryRepository.GetNavigationCategoriesAsync(cancellation, categoryId);

            return _mapper.Map<List<CategoryDto>>(navigationCategories);
        }

        public async Task AddAsync(CategoryDto categoryDto, CancellationToken cancellation)
        {
            var category = _mapper.Map<Category>(categoryDto);

            await _categoryRepository.AddAsync(category, cancellation);
        }

        public async Task UpdateAsync(CategoryDto categoryDto, CancellationToken cancellation)
        {
            var category = _mapper.Map<Category>(categoryDto);

            await _categoryRepository.UpdateAsync(category, cancellation);
        }

        public async Task DeleteAsync(int categoryId, CancellationToken cancellation)
        {
            var category = new Category { Id = categoryId };

            await _categoryRepository.DeleteAsync(category, cancellation);
        }

        public async Task<List<CategoryDto>> GetWithoutSubcategories(CancellationToken cancellation)
        {
            var independentCategories = await _categoryRepository.GetWithoutSubs(cancellation);

            return _mapper.Map<List<CategoryDto>>(independentCategories);
        }
    }
}
