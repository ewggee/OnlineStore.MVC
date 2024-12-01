using AutoMapper;
using OnlineStore.Contracts.Images;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Mappings
{
    public class ImageMappingProfile : Profile
    {
        public ImageMappingProfile()
        {
            CreateMap<ProductImage, ImageDto>()
                .ForMember(
                    dest => dest.Data,
                    opt => opt.MapFrom(src => src.Content))
                .ReverseMap();
        }
    }
}
