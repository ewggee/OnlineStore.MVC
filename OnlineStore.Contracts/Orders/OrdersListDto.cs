using OnlineStore.Contracts.Common;
using OnlineStore.Contracts.Enums;

namespace OnlineStore.Contracts.Orders
{
    public class OrdersListDto : PagedResponse<OrderDto>
    {
        public string? UserEmail { get; set; }

        public OrdersStatusEnum[]? Statuses { get; set; }

        public int[] StatusesIds { get; set; } = [];

        public OrdersSortEnum? Sorting { get; set; }
    }
}
