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
        Task<List<Order>> GetOrdersByUserId(int userId, CancellationToken cancellation);
    }
}
