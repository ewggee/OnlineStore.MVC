using OnlineStore.Core.Common;
using OnlineStore.Core.Products.Models;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Products.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsAsync(GetProductsRequest request, CancellationToken cancellation);

        Task<int> GetProductsTotalCountAsync(CancellationToken cancellation);
    }
}
