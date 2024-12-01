using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        /// <summary>
        /// Каналы уведомления пользователя.
        /// </summary>
        public ICollection<NotificationChannel> NotificationChannels { get; set; } = [];
    }
}
