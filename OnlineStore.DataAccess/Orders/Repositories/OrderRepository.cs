using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Orders.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Orders.Repositories
{
    public class OrderRepository(MutableOnlineStoreDbContext mutableDbContext,
        ReadonlyOnlineStoreDbContext readOnlyDbContext)
        : RepositoryBase<Order>(mutableDbContext, readOnlyDbContext), IOrderRepository
    {
        public Task<List<Order>> GetOrdersByUserId(int userId, CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Order>()
                .Where(o => o.UserId == userId)
                .Include(o => o.Status)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellation);
        }
    }
}
