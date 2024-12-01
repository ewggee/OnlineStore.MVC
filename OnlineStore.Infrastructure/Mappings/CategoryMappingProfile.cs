using AutoMapper;
using OnlineStore.Contracts.Categories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Mappings
{
    public sealed class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
