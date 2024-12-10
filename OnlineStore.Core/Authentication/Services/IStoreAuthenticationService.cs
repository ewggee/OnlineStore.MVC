using Microsoft.AspNetCore.Identity;
using OnlineStore.Domain.Entities;

namespace OnlineStore.Core.Authentication.Services
{
    /// <summary>
    /// Интерфейс сервиса аутентификации.
    /// </summary>
    public interface IStoreAuthenticationService
    {
        /// <summary>
        /// Выполняет вход пользователя.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="isPersistent">Запомнить пользователя.</param>
        Task<bool> SignInAsync(string email, string password, bool isPersistent = false);

        /// <summary>
        /// Выполняет вход пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <param name="isPersistent">Запомнить пользователя.</param>
        Task SignInAsync(ApplicationUser user, bool isPersistent = false);

        /// <summary>
        /// Выполняет выход пользователя.
        /// </summary>
        Task SignOutAsync(CancellationToken cancellation);

        /// <summary>
        /// Регистрирует пользователя.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Пароль.</param>
        Task<IdentityResult> RegisterAsync(string email, string password, CancellationToken cancellation);
    }
}
