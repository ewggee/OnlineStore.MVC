using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Core.Authentication.Services
{
    /// <summary>
    /// Интерфейс сервиса аутентификации.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Выполняет вход пользователя.
        /// </summary>
        /// <param name="email">Email пользователя.</param>
        /// <param name="password">Пароль.</param>
        Task<bool> SignInAsync(string email, string password, CancellationToken cancellation);

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
