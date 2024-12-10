using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contracts.ApplicationRoles;
using OnlineStore.Contracts.ApplicationUsers;
using OnlineStore.Core.Authentication.Services;
using OnlineStore.Core.Users.Services;
using OnlineStore.Domain.Entities;
using OnlineStore.MVC.Models;
using System.Security.Claims;

namespace OnlineStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IStoreAuthenticationService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public AccountController(
            IStoreAuthenticationService authenticationService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userService
            )
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            
            return View();
        }

        [HttpPost("UpdateUserInfo")]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(AppUserDto updatedUserDto)
        {
            var result = await _userService.UpdateUserAsync(updatedUserDto);

            if (result.Succeeded)
            {
                RedirectToAction("Profile");
            }

            // ПЕРЕСМОТРЕТЬ
            return BadRequest();
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var loginViewModel = new LoginViewModel
            {
                Shemes = await _signInManager.GetExternalAuthenticationSchemesAsync(),
            };

            return View(loginViewModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            try
            {
                await _authenticationService.SignInAsync(email, password, rememberMe);

                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ошибка. Попробуйте ещё раз.");
                return View();
            }
        }

        public IActionResult ExternalLogin(string provider, string returnUrl = "")
        {
            //$"https://reservedly-delicious-flycatcher.cloudpub.ru/signin-google?ReturnUrl={returnUrl}";
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "", string remoteError = "")
        {
            if (!string.IsNullOrEmpty(remoteError))
            {
                ModelState.AddModelError("", $"Ошибка провайдера: {remoteError}");
                return RedirectToAction("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError("", $"Ошибка провайдера: {remoteError}");
                return RedirectToAction("Login");
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (!string.IsNullOrEmpty(userEmail))
                {
                    var user = await _userManager.FindByEmailAsync(userEmail);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = userEmail,
                            Email = userEmail
                        };

                        await _userManager.CreateAsync(user);
                        await _userManager.AddToRoleAsync(user, AppRoles.USER);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Произошла ошибка, повторите позже.");
            return RedirectToAction("Login");
        }

        [HttpGet("registration")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var result = await _authenticationService.RegisterAsync(email, password, CancellationToken.None);
            if (result.Succeeded)
            {
                await _authenticationService.SignInAsync(email, password);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOutAsync(CancellationToken.None);
            return RedirectToAction("Index", "Home");
        }
    }
}
