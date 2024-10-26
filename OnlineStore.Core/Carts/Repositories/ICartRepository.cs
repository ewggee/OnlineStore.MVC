using OnlineStore.Core.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Carts.Repositories
{
    /// <summary>
    /// Интерфейс репозитория корзин пользователей.
    /// </summary>
    public interface ICartRepository : IRepository<Cart>
    {
        /// <summary>
        /// Получает корзину пользователя по его ID.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        Task<Cart> GetCartByUserAsync(int userId, CancellationToken cancellation);
    }
}
