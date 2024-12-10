using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Images.Repositories;
using OnlineStore.DataAccess.Common;
using OnlineStore.Domain.Entities;

namespace OnlineStore.DataAccess.Images.Repositories
{
    public sealed class ImageRepository(
        MutableOnlineStoreDbContext mutableDbContext,
        ReadonlyOnlineStoreDbContext readOnlyDbContext)
        : RepositoryBase<ProductImage>(mutableDbContext, readOnlyDbContext), IImageRepository
    {
        public Task<int> RemoveImagesWithProductIdNull()
        {
            return MutableDbContext
                .Set<ProductImage>()
                .Where(pi => pi.ProductId == null)
                .ExecuteDeleteAsync();
        }

        public async Task<int> SaveAsync(ProductImage image, CancellationToken cancellation)
        {
            await MutableDbContext.AddAsync(image, cancellation);
            await MutableDbContext.SaveChangesAsync(cancellation);

            return image.Id;
        }
    }
}
