using OnlineStore.Contracts.Orders;

namespace OnlineStore.Core.Orders.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Получает список заказов текущего пользователя.
        /// </summary>
        Task<List<OrderDto>> GetUserOrdersAsync(CancellationToken cancellation);

        /// <summary>
        /// Оформляет новый заказ.
        /// </summary>
        Task CreateAsync(OrderDto orderDto, CancellationToken cancellation);
    }
}
