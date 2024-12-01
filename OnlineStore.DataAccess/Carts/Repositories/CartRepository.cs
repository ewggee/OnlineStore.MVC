using Microsoft.EntityFrameworkCore;
using OnlineStore.Contracts.Enums;
using OnlineStore.Core.Carts.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Carts.Repositories
{
    public sealed class CartRepository(
        MutableOnlineStoreDbContext mutableDbContext,
        ReadonlyOnlineStoreDbContext readOnlyDbContext) 
        : RepositoryBase<Cart>(mutableDbContext, readOnlyDbContext), ICartRepository
    {
        /// <inheritdoc>
        public Task<Cart> GetCartByUserAsync(int userId, CancellationToken cancellation)
        {
            return MutableDbContext.Set<Cart>()
                .Where(c => c.UserId == userId)
                .Where(c => c.StatusId == (int)CartStatusEnum.New)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(cancellation);
        }
    }
}
