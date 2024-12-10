using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Enums;

namespace OnlineStore.Contracts.Orders
{
    public class GetOrdersRequest : PagedRequest
    {
        public int? UserId { get; set; }

        public string? UserEmail { get; set; }

        public OrdersStatusEnum[]? Statuses { get; set; }

        //public int[] StatusesIds { get; set; } = [];

        public OrdersSortEnum? Sort { get; set; }
    }
}
