using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Authentication.Services;

namespace OnlineStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Выполняет авторизацию и аутентификацию пользователя.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (await _authenticationService.SignInAsync(email, password, CancellationToken.None))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOutAsync(CancellationToken.None);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            var result = await _authenticationService.RegisterAsync(email, password, CancellationToken.None);
            if (result.Succeeded)
            {
                await _authenticationService.SignInAsync(email, password, CancellationToken.None);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
