namespace OnlineStore.Contracts.Carts
{
    public class UpdateItemCountRequest
    {
        public int ProductId { get; set; }
        public int NewQuantity { get; set; }
    }
}
