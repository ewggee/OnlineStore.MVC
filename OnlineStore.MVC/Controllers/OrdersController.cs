using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contracts.ApplicationRoles;
using OnlineStore.Core.Orders.Services;

namespace OnlineStore.MVC.Controllers
{
    [Authorize(Roles = AppRoles.USER)]
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellation)
        {
            var orders = await _orderService.GetCurrentUserOrdersAsync(cancellation);

            return View(orders);
        }
    }
}
