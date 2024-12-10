using Microsoft.EntityFrameworkCore;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Orders;
using OnlineStore.Core.Orders.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Orders.Repositories
{
    public class OrderRepository(MutableOnlineStoreDbContext mutableDbContext,
        ReadonlyOnlineStoreDbContext readOnlyDbContext)
        : RepositoryBase<Order>(mutableDbContext, readOnlyDbContext), IOrderRepository
    {
        /// <inheritdoc/>
        public Task<List<Order>> GetOrdersByUserIdAsync(int userId, CancellationToken cancellation)
        {
            return ReadOnlyDbContext
                .Set<Order>()
                .Where(o => o.UserId == userId)
                .Include(o => o.Status)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<List<Order>> GetOrdersByRequestAsync(GetOrdersRequest request, CancellationToken cancellation)
        {
            var query = ReadOnlyDbContext
                    .Set<Order>()
                    .Include(o => o.User)
                    .Include(o => o.Status)
                    .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                    .AsQueryable();

            if (request.UserId.HasValue)
            {
                query = query
                    .Where(o => o.UserId == request.UserId.Value);
            }

            if (request.Statuses?.Length > 0)
            {
                var statusesIds = request.Statuses
                    .Select(os => (int)os)
                    .ToArray();

                query = query
                    .Where(o => statusesIds.Contains(o.StatusId));
            }

            query = request.Sort switch
            {
                OrdersSortEnum.CreatedDateAsc => query.OrderBy(o => o.OrderDate),
                OrdersSortEnum.CreatedDateDesc => query.OrderByDescending(o => o.OrderDate),
                OrdersSortEnum.TotalPriceAsc => query.OrderBy(o => o.TotalPrice),
                OrdersSortEnum.TotalPriceDesc => query.OrderByDescending(o => o.TotalPrice),
                OrdersSortEnum.StatusAsc => query.OrderBy(o => o.StatusId),
                OrdersSortEnum.StatusDesc => query.OrderByDescending(o => o.StatusId),
                _ => query.OrderBy(o => o.Id)
            };

            query = query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            return query.ToListAsync(cancellation);
        }

        /// <inheritdoc/>
        public Task<int> GetOrdersTotalCountAsync(CancellationToken cancellation, int? userId = null)
        {
            if (userId != null)
            {
                return ReadOnlyDbContext
                .Set<Order>()
                .Where(o => o.UserId == userId)
                .CountAsync(cancellation);
            }
            else
            {
                return ReadOnlyDbContext
                .Set<Order>()
                .CountAsync(cancellation);
            }
        }

        /// <inheritdoc/>
        public Task UpdateOrderStatusAsync(Order order, CancellationToken cancellation)
        {
            MutableDbContext.Set<Order>().Attach(order);

            MutableDbContext.Entry(order).Property(p => p.StatusId).IsModified = true;

            return MutableDbContext.SaveChangesAsync(cancellation);
        }
    }
}
