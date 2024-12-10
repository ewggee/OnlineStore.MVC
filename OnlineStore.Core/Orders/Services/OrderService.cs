using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Orders;
using OnlineStore.Core.Common.Extensions;
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
        public async Task<List<OrderDto>> GetCurrentUserOrdersAsync(CancellationToken cancellation)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var orders = await _orderRepository.GetOrdersByUserIdAsync(user!.Id, cancellation)
                ?? [];

            var ordersDtos = _mapper.Map<List<OrderDto>>(orders);

            return ordersDtos;
        }

        /// <inheritdoc/>
        public async Task<OrdersListDto> GetOrdersByRequestAsync(GetOrdersRequest request, CancellationToken cancellation)
        {
            var totalCount = await _orderRepository.GetOrdersTotalCountAsync(cancellation, request.UserId);

            if (totalCount == 0)
            {
                return new OrdersListDto
                {
                    Page = 1,
                    PageSize = 1,
                    Result = [],
                    TotalCount = 0,
                    Statuses = request.Statuses,
                    Sorting = request.Sort
                };
            }

            var orders = await _orderRepository.GetOrdersByRequestAsync(request, cancellation);

            var ordersDtos = _mapper.Map<List<OrderDto>>(orders);

            var statusesIds = request.Statuses?
                .Select(s => (int)s)
                .ToArray()
                ?? [];

            return new OrdersListDto
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Result = ordersDtos,
                TotalCount = totalCount,
                UserEmail = request.UserEmail,
                Statuses = request.Statuses,
                StatusesIds = statusesIds,
                Sorting = request.Sort
            };
        }

        /// <inheritdoc/>
        public async Task CreateAsync(OrderDto orderDto, CancellationToken cancellation)
        {
            var order = _mapper.Map<Order>(orderDto);

            await _orderRepository.AddAsync(order, cancellation);
        }

        /// <inheritdoc/>
        public async Task UpdateOrderStatusAsync(int orderId, int statusId, CancellationToken cancellation)
        {
            var order = await _orderRepository.GetAsync(orderId);

            order!.StatusId = statusId;

            await _orderRepository.UpdateOrderStatusAsync(order, cancellation);
        }
    }
}
