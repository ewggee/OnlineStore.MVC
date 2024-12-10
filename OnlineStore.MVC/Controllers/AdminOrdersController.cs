using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using OnlineStore.Contracts.ApplicationRoles;
using OnlineStore.Contracts.Enums;
using OnlineStore.Contracts.Orders;
using OnlineStore.Core.Common.Extensions;
using OnlineStore.Core.Common.Models;
using OnlineStore.Core.Orders.Services;
using OnlineStore.Domain.Entities;

namespace OnlineStore.MVC.Controllers
{
    [Authorize(Roles = AppRoles.ADMIN)]
    [Route("admin/orders")]
    public class AdminOrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PaginationOptions _paginationOptions;

        public AdminOrdersController(
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IOptions<PaginationOptions> paginationOptions)
        {
            _orderService = orderService;
            _userManager = userManager;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<IActionResult> Index(GetOrdersRequest request, CancellationToken cancellation)
        {
            request.PageSize = _paginationOptions.OrdersPageSize;

            var isUserEmailSearched = !string.IsNullOrEmpty(request.UserEmail);

            var user = isUserEmailSearched
                ? await _userManager.FindByEmailAsync(request.UserEmail!)
                : null;

            if (isUserEmailSearched && user == null)
            {
                // По вашему запросу ничего не найдено
                return NoContent();
            }

            request.UserId = user?.Id;
            var ordersListDto = await _orderService.GetOrdersByRequestAsync(request, cancellation);

            InitializeViewBags();

            return View(ordersListDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, int statusId, CancellationToken cancellation)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, statusId, cancellation);

            return Ok();
        }

        [NonAction]
        private void InitializeViewBags()
        {
            // ViewBag статусов заказа.
            var statusesList = new List<SelectListItem>();
            foreach(OrdersStatusEnum status in Enum.GetValues(typeof(OrdersStatusEnum)))
            {
                statusesList.Add(new SelectListItem
                {
                    Value = ((int)status).ToString(),
                    Text = EnumExtensions.GetEnumDescription(status)
                });
            }
            ViewBag.Statuses = statusesList;

            // ViewBag методов сортировки.
            var sortingMethodsList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Default", Text = "По умолчанию" }
            };
            foreach (OrdersSortEnum method in Enum.GetValues(typeof(OrdersSortEnum)))
            {
                sortingMethodsList.Add(new SelectListItem
                {
                    Value = method.ToString(),
                    Text = EnumExtensions.GetEnumDescription(method)
                });
            }
            ViewBag.SortingMethod = sortingMethodsList;
        }
    }
}
