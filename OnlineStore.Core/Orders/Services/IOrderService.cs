using OnlineStore.Contracts.Orders;

namespace OnlineStore.Core.Orders.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Возвращает список заказов текущего пользователя.
        /// </summary>
        Task<List<OrderDto>> GetCurrentUserOrdersAsync(CancellationToken cancellation);

        /// <summary>
        /// Возвращает пагинированный список заказов по запросу.
        /// </summary>
        /// <param name="request">Запрос.</param>
        Task<OrdersListDto> GetOrdersByRequestAsync(GetOrdersRequest request, CancellationToken cancellation);

        /// <summary>
        /// Оформляет новый заказ.
        /// </summary>
        Task CreateAsync(OrderDto orderDto, CancellationToken cancellation);

        /// <summary>
        /// Обновляет статус заказа.
        /// </summary>
        /// <param name="orderId">ID заказа.</param>
        /// <param name="statusId">ID статуса.</param>
        Task UpdateOrderStatusAsync(int orderId, int statusId, CancellationToken cancellation);
    }
}
