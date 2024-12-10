using AutoMapper;
using OnlineStore.Contracts.Orders;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Infrastructure.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderStatusName, o => o.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.UserEmail, o => o.MapFrom(src => src.User.Email));

            CreateMap<OrderDto, Order>();
        }
    }
}
