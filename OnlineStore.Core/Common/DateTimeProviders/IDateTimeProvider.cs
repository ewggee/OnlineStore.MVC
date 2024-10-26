namespace OnlineStore.Core.Common.DateTimeProviders
{
    /// <summary>
    /// Интерфейс провайдера времени.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Текущее время по UTC.
        /// </summary>
        DateTime UtcNow { get; }
    }
}
