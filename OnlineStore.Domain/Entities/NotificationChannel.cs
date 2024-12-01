namespace OnlineStore.Domain.Entities
{
    /// <summary>
    /// Способы оповещения пользователя.
    /// </summary>
    public sealed class NotificationChannel
    {
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
    }
}
