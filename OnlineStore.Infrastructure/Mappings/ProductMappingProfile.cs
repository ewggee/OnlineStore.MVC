using AutoMapper;
using OnlineStore.Contracts.Products;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ShortProductDto, Product>()
                .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.MainImageUrl))
                .ReverseMap();
        }
    }
}
