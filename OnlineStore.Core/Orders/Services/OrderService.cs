using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Contracts.Orders;
using OnlineStore.Core.Orders.Repositories;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<List<OrderDto>> GetUserOrdersAsync(CancellationToken cancellation)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var orders = await _orderRepository.GetOrdersByUserId(user.Id, cancellation)
                ?? [];

            var ordersDtos = _mapper.Map<List<OrderDto>>(orders);

            return ordersDtos;
        }

        /// <inheritdoc/>
        public async Task CreateAsync(OrderDto orderDto, CancellationToken cancellation)
        {
            var order = _mapper.Map<Order>(orderDto);

            await _orderRepository.AddAsync(order, cancellation);
        }
    }
}
