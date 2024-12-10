using AutoMapper;
using OnlineStore.Contracts.Orders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Mappings
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, o => o.MapFrom(src => src.Product.Name));

            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
