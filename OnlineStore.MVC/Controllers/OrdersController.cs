﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Orders.Services;

namespace OnlineStore.MVC.Controllers
{
    [Authorize(Roles = "user")]
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
            var orders = await _orderService.GetUserOrdersAsync(cancellation);

            return View(orders);
        }
    }
}
