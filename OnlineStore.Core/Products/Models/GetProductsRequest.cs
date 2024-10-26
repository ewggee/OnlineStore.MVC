namespace OnlineStore.Core.Products.Models
{
    public sealed class GetProductsRequest
    {
        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IncludeCategory { get; set; }

        public bool IncludeImages { get; set; }
    }
}
