using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contracts.Carts;
using OnlineStore.Core.Carts.Services;

namespace OnlineStore.MVC.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(
            ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellation)
        {
            var cart = await _cartService.GetCartAsync(cancellation);

            return View(cart);
        }

        [HttpGet("GetCartItemCount")]
        public async Task<IActionResult> GetCartItemCount(CancellationToken cancellation)
        {
            var cartItemCount = await _cartService.GetCartItemCountAsync(cancellation);

            return Json(new { cartItemCount });
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId, CancellationToken cancellation)
        {
            await _cartService.AddProductToCartAsync(productId, cancellation);
            
            return Ok();
        }

        [HttpPost("UpdateItemCount")]
        public async Task<IActionResult> UpdateItemCount([FromBody] UpdateItemCountRequest request, CancellationToken cancellation)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var success = await _cartService.UpdateItemCountAsync(request.ProductId, request.NewQuantity, cancellation);
            if (success == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("RemoveFromCart")]
        public async Task<IActionResult> RemoveFromCart(int productId, CancellationToken cancellation)
        {
            await _cartService.RemoveItemAsync(productId, cancellation);

            return RedirectToAction("Index");
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(CancellationToken cancellation)
        {
            await _cartService.CheckoutAsync(cancellation);

            return RedirectToAction("Index", "Orders");
        }
    }
}
