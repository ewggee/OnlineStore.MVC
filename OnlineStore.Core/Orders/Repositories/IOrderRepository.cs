using OnlineStore.Contracts.Orders;
using OnlineStore.Core.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Orders.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        /// <summary>
        /// Возвращает все заказы пользователя по его ID.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        Task<List<Order>> GetOrdersByUserIdAsync(int userId, CancellationToken cancellation);

        /// <summary>
        /// Возвращает заказы по запросу.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns></returns>
        Task<List<Order>> GetOrdersByRequestAsync(GetOrdersRequest request, CancellationToken cancellation);

        /// <summary>
        /// Возвращает количество всех заказов или конкретного пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        Task<int> GetOrdersTotalCountAsync(CancellationToken cancellation, int? userId = null);

        /// <summary>
        /// Обновляет поле статуса у заказа.
        /// </summary>
        /// <param name="order">Заказ.</param>
        Task UpdateOrderStatusAsync(Order order, CancellationToken cancellation);
    }
}
