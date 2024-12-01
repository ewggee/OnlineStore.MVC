using OnlineStore.Contracts.Products;

namespace OnlineStore.ApiClient
{
    public class OnlineStoreApiClient : IOnlineStoreApiClient
    {
        private readonly HttpClient _httpClient;

        public OnlineStoreApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task AddProductAsync(ShortProductDto productDto, CancellationToken cancellation)
        {
            var json = 
            var response = _httpClient.PostAsync("Products/add/product", );
        }
    }
}
