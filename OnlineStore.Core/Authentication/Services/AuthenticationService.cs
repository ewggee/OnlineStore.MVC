using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Contracts.ApplicationRoles;
using OnlineStore.Domain.Entities;
using System.Transactions;

namespace OnlineStore.Core.Authentication.Services
{
    /// <summary>
    /// Сервис аутентификации.
    /// </summary>
    public sealed class AuthenticationService : IStoreAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <inheritdoc/>
        public AuthenticationService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> RegisterAsync(string email, string password, CancellationToken cancellation)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            IdentityResult result = new();
            using (var transaction = new TransactionScope(
                TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddToRoleAsync(user, AppRoles.USER);
                    }

                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Откат миграции: {ex.Message}");
                    throw;
                }
            }
               
            return result;
        }

        /// <inheritdoc/>
        public async Task<bool> SignInAsync(string email, string password, bool rememberMe = false)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UnauthorizedAccessException("Неверный email или пароль.");
            
            var isPasswordMatched = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordMatched)
            {
                throw new UnauthorizedAccessException("Неверный email или пароль.");
            }

            await _signInManager.SignInAsync(user, rememberMe);
            return true;
        }

        /// <inheritdoc/>
        public async Task SignInAsync(ApplicationUser user, bool isPersistent = false)
        {
            await _signInManager.SignInAsync(user, isPersistent: isPersistent);
        }

        /// <inheritdoc/>
        public Task SignOutAsync(CancellationToken cancellation)
        {
            return _signInManager.SignOutAsync();
        }
    }
}
