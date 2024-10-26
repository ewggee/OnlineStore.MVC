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
        public Task<ProductImage?> GetByUrlAsync(string url, CancellationToken cancellation)
        {
            return MutableDbContext.Set<ProductImage>()
                .FirstOrDefaultAsync(i => i.Url == url, cancellation);
        }

        public async Task<int> SaveAsync(ProductImage image, CancellationToken cancellation)
        {
            await MutableDbContext.AddAsync(image, cancellation);
            await MutableDbContext.SaveChangesAsync(cancellation);

            return image.Id;
        }
    }
}
