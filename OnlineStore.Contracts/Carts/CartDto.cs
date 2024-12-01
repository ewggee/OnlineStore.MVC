namespace OnlineStore.Contracts.Carts
{
    public sealed class CartDto
    {
        public List<CartItemDto> Items { get; set; } = [];

        public decimal TotalPrice { get; set; }
    }
}
